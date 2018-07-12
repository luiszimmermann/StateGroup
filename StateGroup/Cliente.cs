using System;
using System.Collections.Generic;
using System.Text;
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
}
