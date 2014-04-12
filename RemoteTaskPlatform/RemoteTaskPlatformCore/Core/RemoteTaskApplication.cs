using RemoteTaskPlatform.Contract;
using System;

namespace RemoteTaskPlatform.Core
{
	/// <summary>
	/// 处理请求的具体类
	/// </summary>
	public class RemoteTaskApplication
	{

		/// <summary>
		/// 开始请求的事件
		/// </summary>
		public event EventHandler<RemoteTaskEventArgs> OnRequest;

		/// <summary>
		/// 结束请求事件
		/// </summary>
		public event EventHandler<RemoteTaskEventArgs> EndRequest;

		/// <summary>
		/// 开始处理请求
		/// </summary>
		/// <param name="remoteTaskRequest">请求信息</param>
		public void Process(IRemoteTaskRequest remoteTaskRequest)
		{
			//加载所有模块和模块中绑定的事件处理程序
			this.InitModules(this);
			//创建一个事件参数
			var retmoeTaskEventArgs = new RemoteTaskEventArgs() { RemoteTaskRequest = remoteTaskRequest };
			//调用开始请求事件
			if (this.OnRequest != default(EventHandler<RemoteTaskEventArgs>))
			{
				this.OnRequest(this, retmoeTaskEventArgs);
			}
			//TODO:实际处理逻辑，这里将要调用封住的Roslyn酷进行动态编译
			//调用结束请求事件
			if (this.EndRequest != default(EventHandler<RemoteTaskEventArgs>))
			{
				this.EndRequest(this, retmoeTaskEventArgs);
			}
		}

		/// <summary>
		/// 初始化所有的模块
		/// </summary>
		/// <param name="remoteTaskApplication"></param>
		private void InitModules(RemoteTaskApplication remoteTaskApplication)
		{
			var modules = MudoleGenerator.GetModules();
			foreach (var module in modules)
			{
				module.Init(remoteTaskApplication);
			}
		}
	}
}
