using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Main.Domain.Items
{
    public class ItemRepository : RepositoryBase<ItemId, Item>
    {
        public ItemRepository() : base(x => x.Id)
        {

        }
    }
}
