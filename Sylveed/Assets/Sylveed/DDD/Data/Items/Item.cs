using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Sylveed.DDD.Data.Items
{
	public class Item
	{
		public ItemId Id { get; }
        public string Name { get; }
        public ItemType ItemType { get; }

        public Item(ItemId id, string name, ItemType itemType)
		{
            Id = id;
            Name = name;
            ItemType = itemType;
		}
	}
}
