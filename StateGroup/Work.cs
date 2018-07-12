using System;
using System.Collections.Generic;
using System.Text;

namespace StateGroup
{
	public class Work
	{
		public Work(string path)
		{
			_path = path;
		}

		private readonly string _path;
		public string Path { get => _path; }

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
	}
}
