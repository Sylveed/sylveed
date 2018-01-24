using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Main.Infrastructure;

namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
    public class SkillRepository : RepositoryBase<SkillId, Skill>
    {
        public SkillRepository() : base(x => x.Id)
        {

        }
    }
}
