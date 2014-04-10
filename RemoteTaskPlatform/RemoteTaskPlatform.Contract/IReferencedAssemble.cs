namespace RemoteTaskPlatform.Contract
{
	public interface IReferencedAssemble
	{
		byte[] ReferencedAssembleBytes { get; set; }

		string ReferencedAssembleName { get; set; }
	}
}