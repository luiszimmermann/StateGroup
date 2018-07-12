using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace StateGroup
{
	public class Cliente
	{
		[JsonProperty("estado")]
		public string Estado { get; set; }
		[JsonProperty("cidade")]
		public string Cidade { get; set; }
		[JsonProperty("nome")]
		public string Nome { get; set; }
	}

	public sealed class ClienteMap : ClassMap<Cliente>
	{
		public ClienteMap()
		{
			Map(m => m.Estado).Name("estado");
			Map(m => m.Cidade).Name("cidade");
			Map(m => m.Nome).Name("nome");
		}
	}
}
