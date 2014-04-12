using RemoteTaskPlatform.Contract;
using System;

namespace RemoteTaskPlatform.Core
{
	public class RemoteTaskEventArgs : EventArgs
	{
		public IRemoteTaskRequest RemoteTaskRequest { get; set; }
	}
}
