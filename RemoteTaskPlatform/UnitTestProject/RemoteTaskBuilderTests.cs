using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteTaskPlatform;
using RemoteTaskPlatform.Contract;
using RemoteTaskPlatform.DefaultEntities;
using System;
using System.Reflection;

namespace UnitTestProject
{
	[TestClass()]
	public class RemoteTaskBuilderTests
	{
		[TestMethod()]
		public void BuildDynamicAssembleTest()
		{
			string code;
			using (var fileStream = new FileStream(@"C:\Users\Sago\Source\Repos\RemoteTaskPlatform\RemoteTaskPlatform\UnitTestProject\TestFiles\TestFile.cs", FileMode.Open, FileAccess.Read))
			{
				using (var streamReader = new StreamReader(fileStream, Encoding.Default))
				{
					code = streamReader.ReadToEnd();
				}
			}
			var remoteTaskBuilder = new RemoteTaskBuilder();
			var remoteTask = new DefaultRemoteTask()
			{
				CodeFile = new DefaultCodeFile(code, "TestFile.cs"),
				RemoteTaskIdentity = Guid.NewGuid()
			};
			remoteTaskBuilder.BuildDynamicAssemble(remoteTask);
		}

		[TestMethod]
		public void InvokeDynamicMethodFromDynamicAssemble()
		{
			var assemblePath = @"C:\Users\Sago\Source\Repos\RemoteTaskPlatform\RemoteTaskPlatform\UnitTestProject\bin\Debug\REMOTETASKS\63fc6ba2-5c79-4d8f-bd80-01a4c145376b\RemoteTaskAutoBuildDll.dll";
			Assembly assemble;
			try
			{
				assemble = Assembly.LoadFile(assemblePath);

			}
			catch (Exception)
			{

				throw;
			}
			foreach (var result1 in assemble.GetTypes())
			{

			}
			var calculatorClass = assemble.GetType("UnitTestProject.TestFiles.TestFile");
			var types = assemble.GetTypes();
			var evaluateMethod = calculatorClass.GetMethod("RemoteTaskMethod");
			var result = evaluateMethod.Invoke(null, null).ToString();
			Trace.WriteLine(result);
		}
	}
}
