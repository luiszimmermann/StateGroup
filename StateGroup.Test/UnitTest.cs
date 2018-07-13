using System;
using Xunit;
using StateGroup;
using System.Net;
using System.Linq;

namespace StateGroup.Test
{
	public class UnitTest
	{
		[Fact]
		public void TestHttp()
		{
			var w = new Work("https://gist.githubusercontent.com/israelbgf/fbdb325cd35bc5b956b2e350d354648a/raw/b26d28f4c01a1ec7298020e88a200d292293ae4b/conteudojson");
			Assert.False(w.IsLocalPath);
		}

		[Trait("Linux", "true")]
		[Fact]
		public void TestLocal()
		{
			var w = new Work("/home/luis/conteudojson");
			Assert.True(w.IsLocalPath);
		}

		[Fact]
		public void HttpExists()
		{
			var w = new Work("https://gist.githubusercontent.com/israelbgf/fbdb325cd35bc5b956b2e350d354648a/raw/b26d28f4c01a1ec7298020e88a200d292293ae4b/conteudojson");
			Assert.True(w.FileExists());
		}

		[Fact]
		public void HttpDontExists()
		{
			var w = new Work("https://errorgist.githubusercontent.com/israelbgf/fbdb325cd35bc5b956b2e350d354648a/raw/b26d28f4c01a1ec7298020e88a200d292293ae4b/conteudojson");
			Assert.False(w.FileExists());
		}

		[Trait("Linux", "true")]
		[Fact]
		public void LocalDontExists()
		{
			var w = new Work("/home/teste/testecsv");
			Assert.False(w.FileExists());
		}

		[Fact]
		public void HttpIsJson()
		{
			var w = new Work("https://gist.githubusercontent.com/israelbgf/fbdb325cd35bc5b956b2e350d354648a/raw/b26d28f4c01a1ec7298020e88a200d292293ae4b/conteudojson");
			using (WebClient client = new WebClient())
			{
				var file = client.DownloadString(w.Path);
				Assert.True(w.IsJson(file));
			}
		}

		[Fact]
		public void ClientCountTest()
		{
			var worker = new Work("https://gist.githubusercontent.com/israelbgf/fbdb325cd35bc5b956b2e350d354648a/raw/b26d28f4c01a1ec7298020e88a200d292293ae4b/conteudojson");
			var results = worker.GetAndProcessFile();
			Assert.Equal(14, results.Count);
		}

		[Fact]
		public void StateCountTest()
		{
			var worker = new Work("https://gist.githubusercontent.com/israelbgf/fbdb325cd35bc5b956b2e350d354648a/raw/b26d28f4c01a1ec7298020e88a200d292293ae4b/conteudojson");
			var results = worker.GetAndProcessFile();
			var grouped = results.GroupBy(x => x.State.Trim());
			Assert.Equal(3, grouped.Count());
		}

		[Fact]
		public void InvalidFileTest()
		{
			var worker = new Work("https://raw.githubusercontent.com/luiszimmermann/StateGroup/dev/Files/invalidfile");
			var results = worker.GetAndProcessFile();
			Assert.Empty(results);
		}
	}
}
