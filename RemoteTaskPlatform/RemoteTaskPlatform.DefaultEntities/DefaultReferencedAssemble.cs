using RemoteTaskPlatform.Contract;

namespace RemoteTaskPlatform.DefaultEntities
{
	public class DefaultReferencedAssemble : IReferencedAssemble
	{
		public DefaultReferencedAssemble(byte[] referencedAssembleBytes, string referencedAssembleName)
		{
			this.ReferencedAssembleBytes = referencedAssembleBytes;
			this.ReferencedAssembleName = referencedAssembleName;
		}

		public byte[] ReferencedAssembleBytes { get; set; }

		public string ReferencedAssembleName { get; set; }
	}
}
