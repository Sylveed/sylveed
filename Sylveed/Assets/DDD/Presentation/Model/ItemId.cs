using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sylveed.DDD.Presentation.Helpers;


namespace Sylveed.DDD.Presentation.Model
{
	public class ItemId : Identity<ItemId, int>
	{
		public ItemId(int value) : base(value)
		{
		}
	}
}
