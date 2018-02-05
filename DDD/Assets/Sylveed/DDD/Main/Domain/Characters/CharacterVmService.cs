using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Data.Characters;
using Assets.Sylveed.DDD.Main.Domain.Players;

namespace Assets.Sylveed.DDD.Main.Domain.Characters
{
	public class CharacterVmService
    {
        [Inject]
        readonly ICharacterVmFactory factory;
        [Inject]
        readonly CharacterVmStorage storage;
		[Inject]
		readonly PlayerStorage playerStorage;

		public IEnumerable<CharacterVm> Items => storage.Items;

		public CharacterVm Create(IPlayer player)
		{
			return storage.Add(factory.Create(new CharacterVmId(), player));
		}

		public CharacterVm Get(CharacterVmId id)
		{
			return storage.Get(id);
		}

		public CharacterVm GetWithPlayerId(PlayerId id)
		{
			return storage.PlayerIdIndex.Get(id).SingleOrDefault();
		}

		public CharacterVm GetLocalUser()
		{
			return GetWithPlayerId(playerStorage.GetLocalUserPlayer().Id);
		}
	}
}
