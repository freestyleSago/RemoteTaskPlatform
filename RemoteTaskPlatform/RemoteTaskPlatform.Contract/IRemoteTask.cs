using System;
using System.Collections.Generic;

namespace RemoteTaskPlatform.Contract
{
	public interface IRemoteTask
	{
		ICodeFile CodeFile { get; set; }

		IEnumerable<IReferencedAssemble> ReferencedAssembles { get; set; }

		Guid RemoteTaskIdentity { get; set; }
	}
}