using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Data.Characters;

namespace Assets.Sylveed.DDD.Main.Domain.Players
{
	public class PlayerService
    {
        [Inject]
        readonly PlayerStorage storage;

		PlayerId localUserPlayerId;

		[Inject]
		void Initialize()
		{
			localUserPlayerId = new PlayerId();

			storage.Add(new UserPlayer(localUserPlayerId, "TestUser", new CharacterId(1)));
			storage.Add(new CpuPlayer(new PlayerId(), "Cpu1", new CharacterId(1)));
			storage.Add(new CpuPlayer(new PlayerId(), "Cpu2", new CharacterId(2)));
			storage.Add(new CpuPlayer(new PlayerId(), "Cpu3", new CharacterId(3)));
		}

		public IEnumerable<IPlayer> Items { get { return storage.Items; } }

		public IPlayer Get(PlayerId id)
		{
			return storage.Get(id);
		}

		public IPlayer GetLocalUserPlayer()
		{
			return storage.Get(localUserPlayerId);
		}
	}
}
