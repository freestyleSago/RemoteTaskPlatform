namespace RemoteTaskPlatform.Contract
{
	public interface ICodeFile
	{
		byte[] CodeFileBytes { get; set; }

		string CodeFileName { get; set; }
	}
}
