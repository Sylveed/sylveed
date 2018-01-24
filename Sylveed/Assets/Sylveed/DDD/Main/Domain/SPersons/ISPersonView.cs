using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Domain.Skills;

namespace Assets.Sylveed.DDD.Main.Domain.SPersons
{
    public interface ISPersonView
    {
        float Speed { get; set; }
        void MoveTo(Vector3 direction);
        void ShowSkill(Skill skill);
        Vector3 Position { get; }
        float Angle { get; }
    }
}
