using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;


namespace Assets.Sylveed.DDD.Data.Items
{
	public class ItemService
	{
        [Inject]
        readonly ItemStorage storage;

		public IEnumerable<Item> Items
		{
			get { return storage.Items; }
		}
	}
}
