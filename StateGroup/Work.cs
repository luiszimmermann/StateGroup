using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StateGroup
{
	public class Work
	{
		public Work(string path)
		{
			_path = path;
			_isLocalPath = IsLocal();
		}

		private readonly string _path;
		public string Path { get => _path; }

		private readonly bool _isLocalPath;
		public bool IsLocalPath { get => _isLocalPath; }

		public List<Client> GetAndProcessFile()
		{
			var results = new List<Client>();
			if (FileExists())
			{
				var file = string.Empty;

				if (IsLocalPath)
				{
					file = File.ReadAllText(_path);
				}
				else
				{
					using (WebClient client = new WebClient())
					{
						file = client.DownloadString(_path);
					}
				}

				if (!string.IsNullOrEmpty(file))
				{
					if (IsJson(file))
					{
						results = JsonConvert.DeserializeObject<List<Client>>(file);
					}
					else
					{
						using (var stream = new MemoryStream())
						using (var reader = new StreamReader(stream))
						using (var writer = new StreamWriter(stream))
						using (var csv = new CsvReader(reader))
						{
							writer.Write(file);
							writer.Flush();
							stream.Position = 0;
							csv.Configuration.RegisterClassMap<ClientMap>();

							results = csv.GetRecords<Client>().ToList();
						}
					}
				}
				else
				{
					Console.WriteLine("Empty file.");
				}
			}
			else
			{
				Console.WriteLine("File not found.");
			}
			return results;
		}

		public bool IsLocal()
		{
			if (_path.ToUpper().StartsWith("HTTP:\\") || _path.ToUpper().StartsWith("HTTPS:\\"))
			{
				return false;
			}

			return new Uri(_path).IsFile;
		}

		public bool FileExists()
		{
			if (IsLocalPath)
			{
				return File.Exists(_path);
			}
			else
			{
				HttpWebResponse response = null;
				var request = (HttpWebRequest)WebRequest.Create(_path);
				request.Method = "HEAD";

				try
				{
					response = (HttpWebResponse)request.GetResponse();
					return true;
				}
				catch (WebException ex)
				{
					return false;
				}
				finally
				{
					if (response != null)
					{
						response.Close();
					}
				}
			}
		}

		public bool IsJson(string file)
		{
			file = file.Trim();
			if ((file.StartsWith("{") && file.EndsWith("}")) || (file.StartsWith("[") && file.EndsWith("]")))
			{
				try
				{
					var obj = JToken.Parse(file);
					return true;
				}
				catch (JsonReaderException jex)
				{
					Console.WriteLine(jex.Message);
					return false;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					return false;
				}
			}
			return false;
		}
	}
}
