using Blogifier.Core.Data;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Blogifier.Tests
{
	public class TestHelper
	{
		public string Slash { get { return Path.DirectorySeparatorChar.ToString(); } }
		public string ContextRoot
		{
			get
			{
				string path = Directory.GetCurrentDirectory();
				return path.Substring(0, path.IndexOf($"tests{Slash}Blogifier.Tests"));
			}
		}
        private string GetDataSource()
		{
			return $"DataSource={ContextRoot}src{Slash}Blogifier{Slash}Blog.db";
		}
	}
}
