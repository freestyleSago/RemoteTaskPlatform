using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace RemoteTaskPlatform
{
	/// <summary>
	/// 对象池
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ObjectPool<T> where T : class ,new()
	{
		public ObjectPool(Func<T> objGeneratorFuncint, int poolSize = 10)
		{
			if (objGeneratorFuncint == null)
			{
				throw new ArgumentException("创建对象的方法不能为空", "objGeneratorFuncint");
			}
			this.objGeneratorFunc = objGeneratorFuncint;
			this.PoolSize = poolSize;
		}

		private readonly object locker = new object();

		private readonly Func<T> objGeneratorFunc;

		private readonly ConcurrentBag<T> objectPoll = new ConcurrentBag<T>();

		private int PoolSize { get; set; }

		public void InPool(T obj)
		{
			if (this.objectPoll.Count >= this.PoolSize)
			{
				throw new ArgumentOutOfRangeException("obj", "当前对象池已满，不能再添加新的对象");
			}
			this.objectPoll.Add(obj);
		}

		public void InPool(IEnumerable<T> objs)
		{
			if (objs.Count() > this.PoolSize - this.objectPoll.Count)
			{
				throw new ArgumentOutOfRangeException("objs", "当前对象池已满，不能再添加新的对象");
			}
			foreach (var obj in objs)
			{
				this.InPool(obj);
			}
		}

		public T OutPool()
		{
			lock (this.locker)
			{
				T obj;
				while (!this.objectPoll.TryTake(out obj))
				{
					if (this.objectPoll.Count < this.PoolSize)
					{
						this.InPool(this.objGeneratorFunc());
					}
				}
				return obj;
			}
		}
	}
}
