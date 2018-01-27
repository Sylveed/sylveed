using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Main.Domain.Characters
{
    public class CharacterVmStorage : StorageBase<CharacterVmId, CharacterVm>
    {
        public CharacterVmStorage() : base(x => x.Id)
        {

        }
    }
}
