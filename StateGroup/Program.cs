using System;
using System.Linq;

namespace StateGroup
{
	public class Program
	{
		static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				var path = args[0].Trim();
				var w = new Work(path);
				var results = w.GetAndProcessFile();

				var grouped = results.GroupBy(x => x.Estado.Trim());

				foreach (var g in grouped.OrderByDescending(x => x.Count()))
				{
					Console.WriteLine(string.Concat(g.Key, ": ", g.Count()));
				}
			}
		}
	}
}
