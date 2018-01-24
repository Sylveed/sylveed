using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Data.Items
{
	public class ItemId : Identity<ItemId, int>
	{
		public ItemId(int value) : base(value)
		{
		}
	}
}
