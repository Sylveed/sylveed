using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Sylveed.DDD.Main.Domain.Items
{
	public class ItemVm
	{
        readonly IView view;

		public ItemVmId Id { get; }

		public ItemVm(ItemVmId id, IView view)
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
