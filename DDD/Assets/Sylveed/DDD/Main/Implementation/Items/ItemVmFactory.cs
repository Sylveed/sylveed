using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Main.Domain.Items;
using Assets.Sylveed.DDD.Main.Application;
using Assets.Sylveed.DDD.Data.Items;

namespace Assets.Sylveed.DDD.Main.Implementation.Items
{
    public class ItemVmFactory : IItemVmFactory
	{
		[Inject]
		readonly ItemService itemService;

		public ItemVm Create()
		{
			throw new NotImplementedException();
		}
	}
}
