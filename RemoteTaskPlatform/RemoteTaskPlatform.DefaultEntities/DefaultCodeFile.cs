using System.Text;
using RemoteTaskPlatform.Contract;

namespace RemoteTaskPlatform.DefaultEntities
{
	public class DefaultCodeFile : ICodeFile
	{
		public DefaultCodeFile(byte[] codeFileBytes, string CodeFileName)
		{
			this.CodeFileBytes = codeFileBytes;
			this.CodeFileName = CodeFileName;
		}

		public DefaultCodeFile(string codeString, string CodeFileName)
		{
			this.CodeFileBytes = Encoding.Default.GetBytes(codeString);
			this.CodeFileName = CodeFileName;
		}

		public byte[] CodeFileBytes { get; set; }

		public string CodeFileName { get; set; }
	}
}
