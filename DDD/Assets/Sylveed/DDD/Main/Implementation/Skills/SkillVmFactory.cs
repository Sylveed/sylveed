using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.Reactive;

using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Main.Application;
using Assets.Sylveed.DDD.Data.Skills;

namespace Assets.Sylveed.DDD.Main.Implementation.Skills
{
    public class SkillVmFactory : ISkillVmFactory
    {
		[Inject]
		readonly SkillService skillService;

        public IObservable<SkillVm[]> Load()
        {
            return Observable.Return(new[]
            {
                new SkillVm(new SkillVmId(0)),
                new SkillVm(new SkillVmId(1)),
                new SkillVm(new SkillVmId(2)),
            });
        }
    }
}
