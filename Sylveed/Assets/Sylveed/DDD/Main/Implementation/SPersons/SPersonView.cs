using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Domain.SPersons;
using Assets.Sylveed.DDD.Main.Domain.Skills;

namespace Assets.Sylveed.DDD.Main.Implementation.SPersons
{
    public class SPersonView : MonoBehaviour, ISPersonView
    {
        public float Speed
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Vector3 Position
        {
            get { return transform.position; }
        }

        public float Angle
        {
            get { return transform.localEulerAngles.y; }
        }

        public void MoveTo(Vector3 direction)
        {
            throw new NotImplementedException();
        }

        public void ShowSkill(SkillVm skill)
        {
            throw new NotImplementedException();
        }
    }
}
