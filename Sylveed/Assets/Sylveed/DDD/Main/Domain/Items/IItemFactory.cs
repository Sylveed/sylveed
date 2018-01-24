using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.DDD.Main.Domain.Items
{
    public interface IItemFactory
    {
        Item Create();
    }
}
