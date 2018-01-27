using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UniRx;

namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
    public interface ISkillView
    {
		IObservable<Unit> Show();
    }
}
