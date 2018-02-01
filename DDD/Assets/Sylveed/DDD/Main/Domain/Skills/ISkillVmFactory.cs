using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Data.Skills;
using Assets.Sylveed.DDD.Main.Domain.Characters;

namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
    public interface ISkillVmFactory
    {
		SkillVm Create(SkillId skillId, SkillVmId id);
    }
}
