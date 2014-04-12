using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteTaskPlatform.DefaultEntities;

namespace RemoteTaskPlatform.Tests
{
	[TestClass()]
	public class RemoteTaskRuntimeTests
	{
		[TestMethod()]
		public void ProcessTest()
		{
			string code;
			using (var fileStream = new FileStream(@"C:\Users\Sago\Source\Repos\RemoteTaskPlatform\RemoteTaskPlatform\UnitTestProject\TestFiles\TestFile.cs", FileMode.Open, FileAccess.Read))
			{
				using (var streamReader = new StreamReader(fileStream, Encoding.Default))
				{
					code = streamReader.ReadToEnd();
				}
			}
			var remoteTasks = new List<DefaultRemoteTask>()
			{
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask(),
				new DefaultRemoteTask()
			};
			Parallel.ForEach(remoteTasks, RemoteTaskRuntime.Process);
			//var remoteTask = new DefaultRemoteTask()
			//{
			//	CodeFile = new DefaultCodeFile(code, "TestFile.cs"),
			//	RemoteTaskIdentity = Guid.NewGuid()
			//};
		}
	}
}
