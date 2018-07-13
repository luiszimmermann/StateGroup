using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace StateGroup
{
	public class Client
	{
		[JsonProperty("estado")]
		public string State { get; set; }
		[JsonProperty("cidade")]
		public string City { get; set; }
		[JsonProperty("nome")]
		public string Name { get; set; }
	}

	public sealed class ClientMap : ClassMap<Client>
	{
		public ClientMap()
		{
			Map(m => m.State).Name("estado");
			Map(m => m.City).Name("cidade");
			Map(m => m.Name).Name("nome");
		}
	}
}
