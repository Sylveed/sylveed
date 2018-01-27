using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.Reactive;

namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
    public interface ISkillView
    {
		IObservable<Unit> Show();
    }
}
