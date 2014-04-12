using System.Diagnostics;
using RemoteTaskPlatform.Contract;
using RemoteTaskPlatform.Core;
using System;
using System.IO;

namespace RemoteTaskPlatform.Modules
{
	//记录日志模块
	public class LogRemoteTaskModule : IModule
	{
		public void Init(RemoteTaskApplication remoteTaskApplication)
		{
			remoteTaskApplication.OnRequest += this.Proess;
		}

		private object locker = new object();
		public void Proess(object sender, RemoteTaskEventArgs e)
		{
			//lock (this.locker)
			//{
			//	using (var fileStream = new FileStream("C:\\RemoteTaskLog.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
			//	{
			//		using (var streamWrite = new StreamWriter(fileStream))
			//		{
			//			streamWrite.WriteLine("RemoteTask当前处理时间{0,3}", );
			//		}
			//	}

			Trace.WriteLine(DateTime.Now.ToString("yy-MMM-dd ddd"));
		}
	}
}
