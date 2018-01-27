using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.DDDTools
{
	public interface IStorageIndex<TKey, TValue>
	{
		IEnumerable<TValue> Get(TKey key);
		bool Remove(TKey key);
		bool Contains(TKey key);
	}
}
