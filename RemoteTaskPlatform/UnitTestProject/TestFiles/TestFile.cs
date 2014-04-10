using RemoteTaskPlatform.Contract;

namespace UnitTestProject.TestFiles
{
	public class TestFile : IRemoteTaskClass
	{
		public int Count { get; set; }

		public void RemoteTaskMethod()
		{
			this.Count++;
		}
	}
}
