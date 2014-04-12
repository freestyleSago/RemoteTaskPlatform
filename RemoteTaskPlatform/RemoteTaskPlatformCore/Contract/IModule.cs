using RemoteTaskPlatform.Core;

namespace RemoteTaskPlatform.Contract
{
	/// <summary>
	/// 自定义模块的接口
	/// </summary>
	public interface IModule
	{
		void Init(RemoteTaskApplication remoteTaskApplication);

		void Proess(object sender, RemoteTaskEventArgs e);
	}
}
