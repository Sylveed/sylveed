using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sylveed.DDD.Presentation.Helpers
{
	public interface IRepositoryIndexer<in TKey, out TValue>
	{
		TValue Get(TKey key);
		bool Remove(TKey key);
		bool Contains(TKey key);
	}
}
