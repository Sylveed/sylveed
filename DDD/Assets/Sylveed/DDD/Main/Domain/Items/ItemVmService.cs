using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;


namespace Assets.Sylveed.DDD.Main.Domain.Items
{
	public class ItemVmService
	{
        [Inject]
		readonly IItemVmFactory factory;
        [Inject]
        readonly ItemVmStorage storage;

		public ItemVm Create(ItemVm.Parameter parameter)
		{
			return storage.Add(factory.Create());
		}
	}
}
