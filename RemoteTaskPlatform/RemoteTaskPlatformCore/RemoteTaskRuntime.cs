using RemoteTaskPlatform.Contract;
using RemoteTaskPlatform.Core;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RemoteTaskPlatform
{
	/// <summary>
	/// 服务入口类
	/// </summary>
	public static class RemoteTaskRuntime
	{
		static RemoteTaskRuntime()
		{
			remoteApplicationPool.InPool(new List<RemoteTaskApplication>()
			{
				new RemoteTaskApplication(),
			});
		}

		/// <summary>
		/// 请求队列
		/// </summary>
		private static readonly ConcurrentQueue<IRemoteTaskRequest> remoteTaskRequests = new ConcurrentQueue<IRemoteTaskRequest>();

		/// <summary>
		/// 处理请求的对象池
		/// </summary>
		private static readonly ObjectPool<RemoteTaskApplication> remoteApplicationPool = new ObjectPool<RemoteTaskApplication>(() => new RemoteTaskApplication());

		/// <summary>
		/// 开始处理
		/// </summary>
		/// <param name="remoteTask"></param>
		public static void Process(IRemoteTaskRequest remoteTask)
		{
			//将请求加入队列
			remoteTaskRequests.Enqueue(remoteTask);
			if (!remoteTaskRequests.IsEmpty)
			{
				//从RemoteTaskPool中取得对象
				var remoteTaskApplication = remoteApplicationPool.OutPool();
				//从请求队列中将请出列，交由remoteTaskAppliction处理
				IRemoteTaskRequest remoteTaskRequest;
				remoteTaskRequests.TryDequeue(out remoteTaskRequest);
				//将请求交由RemoteTaskApplication处理
				remoteTaskApplication.Process(remoteTaskRequest);
				//请求处理完之后，我们要将RemoteTaskApplication对象释放到对象池中
				remoteApplicationPool.InPool(remoteTaskApplication);
			}
		}
	}
}
