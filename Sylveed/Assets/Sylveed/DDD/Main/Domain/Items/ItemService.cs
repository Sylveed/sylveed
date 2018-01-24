using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;


namespace Assets.Sylveed.DDD.Main.Domain.Items
{
	public class ItemService
	{
        [Inject]
		readonly IItemFactory factory;
        [Inject]
        readonly ItemRepository repository;

		public Item Create(Item.Parameter parameter)
		{
			return repository.Add(factory.Create());
		}
	}
}
