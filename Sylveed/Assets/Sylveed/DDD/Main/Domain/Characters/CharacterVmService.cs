using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;


namespace Assets.Sylveed.DDD.Main.Domain.Characters
{
	public class CharacterVmService
    {
        [Inject]
        readonly ICharacterVmFactory factory;
        [Inject]
        readonly CharacterVmStorage storage;

		CharacterVmId playerId;

		public CharacterVm Player
		{
			get { return playerId == null ? null : storage.Get(playerId); }
		}

		public CharacterVm Create(string name)
		{
			return storage.Add(factory.Create(new CharacterVmId(), name));
		}

		public void SetPlayer(CharacterVmId playerId)
		{
			this.playerId = playerId;
		}
	}
}
