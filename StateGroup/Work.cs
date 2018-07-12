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

		private string _file { get; set; }

		public List<Cliente> GetAndProcessFile()
		{
			var results = new List<Cliente>();
			if (FileExists())
			{
				_file = string.Empty;

				if (!IsLocalPath)
				{
					using (WebClient client = new WebClient())
					{
						_file = client.DownloadString(_path);
					}
				}
				else
				{
					_file = File.ReadAllText(_path);
				}

				if (!string.IsNullOrEmpty(_file))
				{
					if (IsJson(_file))
					{
						results = JsonConvert.DeserializeObject<List<Cliente>>(_file);
					}
					else
					{
						using (var stream = new MemoryStream())
						using (var reader = new StreamReader(stream))
						using (var writer = new StreamWriter(stream))
						using (var csv = new CsvReader(reader))
						{
							writer.Write(_file);
							writer.Flush();
							stream.Position = 0;
							csv.Configuration.RegisterClassMap<ClienteMap>();

							results = csv.GetRecords<Cliente>().ToList();
						}
					}
				}
			}
			return results;
		}

		public bool IsLocal()
		{
			if (_path.ToUpper().StartsWith("HTTP"))
			{
				return false;
			}
			return true;
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

		public bool IsJson(string file = null)
		{
			if (string.IsNullOrEmpty(file)) file = _file;

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
