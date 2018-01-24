using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.DDD.Main.Infrastructure
{
	public interface IRepositoryIndex<in TKey, out TValue>
	{
		IEnumerable<TValue> Get(TKey key);
		bool Remove(TKey key);
		bool Contains(TKey key);
	}
}
