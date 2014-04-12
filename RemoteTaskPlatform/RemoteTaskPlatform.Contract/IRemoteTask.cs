using System;
using System.Collections.Generic;

namespace RemoteTaskPlatform.Contract
{
	public interface IRemoteTaskRequest
	{
		ICodeFile CodeFile { get; set; }

		IEnumerable<IReferencedAssemble> ReferencedAssembles { get; set; }

		Guid RemoteTaskIdentity { get; set; }
	}
}