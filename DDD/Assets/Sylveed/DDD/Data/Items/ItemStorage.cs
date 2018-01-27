using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Data.Items
{
	public class ItemStorage : StorageBase<ItemId, Item>
	{
		public ItemStorage() : base(x => x.Id)
		{

		}
	}
}
