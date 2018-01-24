using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Domain.Skills;

namespace Assets.Sylveed.DDD.Main.Domain.SPersons
{
	public class SPersonVm
	{
        readonly SPersonTrait trait;

        ISPersonView view => trait.View;

        public SPersonVmId Id => trait.Id;

		public Vector3 Position { get { return view.Position; } }

		public float Angle { get { return view.Angle; } }

		public SPersonVm(SPersonTrait trait)
		{
            this.trait = trait;
		}

		public void UseSkill(SPersonVmSkillIndex index)
		{
            view.ShowSkill(trait.SkillSet.GetSkill(index));
		}

		public void MoveTo(Vector3 direction, float speed)
		{
            view.Speed = speed;
            view.MoveTo(direction);
		}
	}
}
