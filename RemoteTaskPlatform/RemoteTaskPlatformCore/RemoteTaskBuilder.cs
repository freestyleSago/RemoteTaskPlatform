using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RemoteTaskPlatform.Contract;
using RemoteTaskPlatform.DefaultEntities;
using System.IO;
using Roslyn.Scripting.CSharp;

namespace RemoteTaskPlatform
{
	public class RemoteTaskBuilder
	{
		public RemoteTaskBuilder(ICloneFilesAndAssembles cloneFilesAndAssembles = default(ICloneFilesAndAssembles))
		{
			this.CloneFilesAndAssembles = cloneFilesAndAssembles == default(ICloneFilesAndAssembles) ? new DefaultCloneFilesAndAssembles() : cloneFilesAndAssembles;
		}

		public ICloneFilesAndAssembles CloneFilesAndAssembles { get; set; }

		public void BuildDynamicAssemble(IRemoteTask remoteTask)
		{
			var filesAndAssemblesDirectoryPath = this.CloneFilesAndAssembles.CloneFilesAndAssembles(remoteTask);
			var code = string.Empty;
			//读取代码文件中的源代码，并动态构造一个程序集
			using (var fileStream = new FileStream(Path.Combine(filesAndAssemblesDirectoryPath, remoteTask.CodeFile.CodeFileName), FileMode.Open, FileAccess.Read))
			{
				using (var streamReader = new StreamReader(fileStream))
				{
					code = streamReader.ReadToEnd();
				}
			}
			//var engine = new ScriptEngine();
			//var session = engine.CreateSession();
			//session.AddReference(typeof(IRemoteTaskClass).Assembly.Location);
			//session.ImportNamespace("System.Linq");
			var syntaxTree = SyntaxFactory.ParseSyntaxTree(code);
			var compilation = CSharpCompilation.Create("RemoteTaskAutoBuildDll.dll", new[] { syntaxTree },
			options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
			references: new[]
			{
				new MetadataFileReference(typeof(object).Assembly.Location),
				new MetadataFileReference(typeof(IRemoteTaskClass).Assembly.Location) 
			});
			var model = compilation.GetSemanticModel(syntaxTree);
			var root = (CompilationUnitSyntax)syntaxTree.GetRoot();
			var nameInfo = model.GetSymbolInfo(root.Usings[0].Name);
			var systemSymbol = (INamespaceSymbol)nameInfo.Symbol;
			foreach (var ns in systemSymbol.GetNamespaceMembers())
			{
				Trace.WriteLine(ns.Name);
			}




			using (var fileStream = new FileStream(Path.Combine(filesAndAssemblesDirectoryPath, "RemoteTaskAutoBuildDll.dll"), FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				compilation.Emit(fileStream);
			}
		}
	}
}
