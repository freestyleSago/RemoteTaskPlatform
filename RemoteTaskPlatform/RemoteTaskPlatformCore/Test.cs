using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;

namespace RemoteTaskPlatform
{
	public class TestClass
	{
		public void Test()
		{
			SyntaxTree tree = SyntaxTree.ParseText
		(@"class Bar { 
            void Foo() { Console.WriteLine(""foo""); }
              }");

			//Find the first method declaration inside the first class declaration
			MethodDeclarationSyntax methodDecl = tree.GetRoot().DescendantNodes()
				.OfType<ClassDeclarationSyntax>()
				.First().ChildNodes().OfType<MethodDeclarationSyntax>().First();

			//Create a compilation unit
			Compilation compilation = Compilation.Create("SimpleMethod").AddSyntaxTrees(tree);


			//Get the associated semantic model of our syntax tree
			var model = compilation.GetSemanticModel(tree);

			//Find the symbol of our Foo method
			Symbol methodSymbol = model.GetDeclaredSymbol(methodDecl);


			//Get the name of the method symbol
			Console.WriteLine(methodSymbol.Name);
		}
	}
}
