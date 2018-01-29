using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Main.Domain.Players;

namespace Assets.Sylveed.DDD.Main.Domain.Characters
{
    public class CharacterVmStorage : StorageBase<CharacterVmId, CharacterVm>
    {
		public IStorageIndex<PlayerId, CharacterVm> PlayerIdIndex { get; }

        public CharacterVmStorage() : base(x => x.Id)
        {
			PlayerIdIndex = CreateIndex(x => x.Player.Id);
		}
    }
}
