﻿using System;
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
				var worker = new Work(path);
				var results = worker.GetAndProcessFile();

				var grouped = results.GroupBy(x => x.State.Trim());

				foreach (var g in grouped.OrderByDescending(x => x.Count()))
				{
					Console.WriteLine(string.Concat(g.Key, ": ", g.Count()));
				}
			}
			else
			{
				Console.WriteLine("StateGroup requires exactly 1 argument.");
			}
		}
	}
}
