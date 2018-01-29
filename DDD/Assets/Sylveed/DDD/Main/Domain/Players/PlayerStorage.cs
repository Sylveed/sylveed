using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Main.Domain.Players
{
    public class PlayerStorage : StorageBase<PlayerId, IPlayer>
    {
		PlayerId localUserPlayerId;

        public PlayerStorage() : base(x => x.Id)
        {

        }

		public IPlayer AddLocalUserPlayer(IPlayer player)
		{
			localUserPlayerId = player.Id;
			return Add(player);
		}

		public IPlayer GetLocalUserPlayer()
		{
			return Get(localUserPlayerId);
		}
    }
}
