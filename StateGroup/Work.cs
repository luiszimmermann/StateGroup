using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

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

		public void GetAndProcessFile()
		{
			throw new NotImplementedException();
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
	}
}
