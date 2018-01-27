using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Main.Domain.Items
{
    public class ItemVmStorage : StorageBase<ItemVmId, ItemVm>
    {
        public ItemVmStorage() : base(x => x.Id)
        {

        }
    }
}
