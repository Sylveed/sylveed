using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
    public class SkillVmStorage : StorageBase<SkillVmId, SkillVm>
    {
        public SkillVmStorage() : base(x => x.Id)
        {

        }
    }
}
