using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using UniRx;
using Assets.Sylveed.Reactive;


namespace Assets.Sylveed.DDDTools
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

        protected IRepositoryIndex<TSubKey, TValue> CreateIndex<TSubKey>(Func<TValue, TSubKey> keySelector)
        {
            return new Index<TSubKey>(this, keySelector);
        }
        
        class Index<TSubKey> : IRepositoryIndex<TSubKey, TValue>
        {
            readonly Dictionary<TSubKey, HashSet<TKey>> keyMap = new Dictionary<TSubKey, HashSet<TKey>>();

            readonly RepositoryBase<TKey, TValue> parent;
            readonly Func<TValue, TSubKey> keySelector;

            public Index(RepositoryBase<TKey, TValue> parent, Func<TValue, TSubKey> keySelector)
            {
                this.parent = parent;
                this.keySelector = keySelector;

                MakeIndex();
            }

            void MakeIndex()
            {
                foreach (var key in parent.Keys)
                {
                    Add(key);
                }

                parent.added.Subscribe(key =>
                {
                    Add(key);
                });

                parent.removed.Subscribe(item =>
                {
                    Remove(item);
                });

                parent.cleared.Subscribe(_ =>
                {
                    keyMap.Clear();
                });
            }

            void Remove(TValue item)
            {
                var key = parent.primaryKeySelector(item);
                var subKey = keySelector(item);
                var keys = keyMap[subKey];

                keys.Remove(key);
                if (keys.Count == 0)
                {
                    keyMap.Remove(subKey);
                }
            }

            void Add(TKey key)
            {
                var value = parent.Get(key);

                var subKey = keySelector(value);

                HashSet<TKey> keys;
                if (!keyMap.TryGetValue(subKey, out keys))
                {
                    keys = new HashSet<TKey>();
                    keyMap.Add(subKey, keys);
                }
                keys.Add(key);
            }

            public IEnumerable<TValue> Get(TSubKey key)
            {
                HashSet<TKey> primaryKeys;
                if (!keyMap.TryGetValue(key, out primaryKeys))
                    throw new InvalidOperationException($"{key} not found.");

                foreach (var x in primaryKeys)
                    yield return parent.Get(x);
            }

            public bool Contains(TSubKey key)
            {
                return keyMap.ContainsKey(key);
            }

            public bool Remove(TSubKey key)
            {
                HashSet<TKey> primaryKeys;
                if (!keyMap.TryGetValue(key, out primaryKeys))
                    return false;

                foreach(var x in primaryKeys.ToArray())
                {
                    parent.Remove(x);
                }

                return true;
            }
        }
    }
}
