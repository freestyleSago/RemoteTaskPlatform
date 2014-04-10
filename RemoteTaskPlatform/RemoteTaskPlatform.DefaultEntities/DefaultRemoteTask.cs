using RemoteTaskPlatform.Contract;
using System;
using System.Collections.Generic;

namespace RemoteTaskPlatform.DefaultEntities
{
	public class DefaultRemoteTask : IRemoteTask
	{
		public ICodeFile CodeFile { get; set; }

		public IEnumerable<IReferencedAssemble> ReferencedAssembles { get; set; }

		public Guid RemoteTaskIdentity { get; set; }
	}
}
