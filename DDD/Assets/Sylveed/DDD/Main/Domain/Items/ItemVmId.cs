using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;


namespace Assets.Sylveed.DDD.Main.Domain.Items
{
	public class ItemVmId : Identity<ItemVmId, int>
	{
		public ItemVmId(int value) : base(value)
		{
		}
	}
}
