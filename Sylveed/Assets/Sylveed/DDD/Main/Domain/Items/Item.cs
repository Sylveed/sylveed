using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.DDD.Main.Domain.Items
{
	public class Item
	{
		readonly IView view;

		public ItemId Id { get; }

		public Item(ItemId id, IView view)
		{
			this.view = view;
			Id = id;
		}

		public interface IView
		{
			
		}

		public class Parameter
		{
			
		}
	}
}
