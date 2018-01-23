using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sylveed.DDD.Presentation.Helpers;
using Sylveed.DDD.Presentation.Model;


namespace Sylveed.DDD.Presentation.Container
{
	public class ItemService
	{
		readonly Factory<Item, Item.Parameter> factory;
		readonly RepositoryBase<ItemId, Item> repository;

		public ItemService(IContainerConfiguration config)
		{
			config.Configure(this);
		}

		public Item Create(Item.Parameter parameter)
		{
			return repository.Add(factory.Create(parameter));
		}
	}
}
