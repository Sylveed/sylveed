using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;


namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
    public interface ISkillVmFactory
    {
        IObservable<SkillVm[]> Load();
    }
}
