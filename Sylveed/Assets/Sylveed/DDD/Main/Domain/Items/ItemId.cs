using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Main.Infrastructure;


namespace Assets.Sylveed.DDD.Main.Domain.Items
{
	public class ItemId : Identity<ItemId, int>
	{
		public ItemId(int value) : base(value)
		{
		}
	}
}
