using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Main.Domain.Players
{
    public class PlayerStorage : StorageBase<PlayerId, IPlayer>
    {
        public PlayerStorage() : base(x => x.Id)
        {

        }
    }
}
