using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using UniRx;
using Sylveed.Reactive;


namespace Sylveed.DDD.Presentation.Helpers
{
	public class RepositoryBase<TKey, TValue>
	{
		readonly Func<TValue, TKey> primaryKeySelector;
		readonly Dictionary<TKey, TValue> map = new Dictionary<TKey, TValue>();

		readonly Subject<TKey> added = new Subject<TKey>();
		readonly Subject<TValue> removed = new Subject<TValue>();
		readonly Subject<Unit> cleared = new Subject<Unit>();

		public IEnumerable<TValue> Items { get { return map.Values; } }
		public IEnumerable<TKey> Keys { get { return map.Keys; } }

		public IObservable<TKey> Added { get { return added; } }
		public IObservable<TValue> Removed { get { return removed; } }
		public IObservable<Unit> Cleared { get { return cleared; } }

		public IObservable<Unit> Changed
		{
			get { return Observable.Merge(added.AsUnitObservable(), removed.AsUnitObservable(), cleared); }
		}

		public RepositoryBase(Func<TValue, TKey> primaryKeySelector)
		{
			this.primaryKeySelector = primaryKeySelector;
		}

		public TValue Get(TKey key)
		{
			return map[key];
		}

		public TValue GetOrDefault(TKey key)
		{
			TValue value;
			if (map.TryGetValue(key, out value))
				return value;

			return default(TValue);
		}

		public bool Contains(TKey key)
		{
			return map.ContainsKey(key);
		}

		public IRepositoryIndexer<TSubKey, TValue> Index<TSubKey>(Func<TValue, TSubKey> keySelector)
		{
			return new RepositoryIndexer<TSubKey>(this, keySelector);
		}

		public TValue Add(TValue item)
		{
			var key = primaryKeySelector(item);

			map.Add(primaryKeySelector(item), item);

			added.OnNext(key);

			return item;
		}

		public bool Remove(TKey key)
		{
			TValue item;
			if (map.TryGetValue(key, out item))
			{
				map.Remove(key);

				removed.OnNext(item);

				return true;
			}

			return false;
		}

		public void Clear()
		{
			map.Clear();

			cleared.OnNext(Unit.Default);
		}

		class RepositoryIndexer<TSubKey> : IRepositoryIndexer<TSubKey, TValue>
		{
			readonly Dictionary<TSubKey, TKey> keyMap = new Dictionary<TSubKey, TKey>();

			readonly RepositoryBase<TKey, TValue> parent;
			readonly Func<TValue, TSubKey> keySelector;

			public RepositoryIndexer(RepositoryBase<TKey, TValue> parent, Func<TValue, TSubKey> keySelector)
			{
				this.parent = parent;
				this.keySelector = keySelector;

				MakeIndex();
			}

			void MakeIndex()
			{
				foreach (var key in parent.Keys)
				{
					var value = parent.Get(key);

					var subKey = keySelector(value);

					keyMap.Add(subKey, key);
				}

				parent.added.Subscribe(key =>
				{
					var value = parent.Get(key);

					var subKey = keySelector(value);

					keyMap.Add(subKey, key);
				});

				parent.removed.Subscribe(item =>
				{
					var subKey = keySelector(item);

					keyMap.Remove(subKey);
				});

				parent.cleared.Subscribe(_ =>
				{
					keyMap.Clear();
				});
			}

			public TValue Get(TSubKey key)
			{
				return parent.Get(GetPrimaryKey(key));
			}

			public bool Contains(TSubKey key)
			{
				return keyMap.ContainsKey(key);
			}

			public bool Remove(TSubKey key)
			{
				return parent.Remove(GetPrimaryKey(key));
			}

			TKey GetPrimaryKey(TSubKey subKey)
			{
				try
				{
					return keyMap[subKey];
				}
				catch (KeyNotFoundException)
				{
					throw new InvalidOperationException($"{subKey} not found.");
				}
			}
		}
	}
}
