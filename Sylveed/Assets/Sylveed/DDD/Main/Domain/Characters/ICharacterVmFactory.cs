using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.DDD.Main.Domain.Characters
{
    public interface ICharacterVmFactory
    {
        CharacterVm Create(CharacterVmId id, string name);
    }
}
