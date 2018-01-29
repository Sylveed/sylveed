using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Data.Characters;
using Assets.Sylveed.DDD.Main.Domain.Players;

namespace Assets.Sylveed.DDD.Main.Domain.Characters
{
    public interface ICharacterVmFactory
    {
        CharacterVm Create(CharacterVmId id, IPlayer player);
    }
}
