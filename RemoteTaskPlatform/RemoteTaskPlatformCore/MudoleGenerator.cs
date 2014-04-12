using RemoteTaskPlatform.Contract;
using RemoteTaskPlatform.Modules;
using System.Collections.Generic;

namespace RemoteTaskPlatform
{
	/// <summary>
	/// 这是类是用来查找所有实现IModule接口的类，暂时写在程序里，以后可以将注册的模块放到配置文件中，类似于Asp.Net的IHttpModule
	/// </summary>
	public static class MudoleGenerator
	{
		public static IEnumerable<IModule> GetModules()
		{
			//TODO：这里暂时返回固定的模块
			return new List<IModule>() { new LogRemoteTaskModule() };
		}
	}
}
