using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Domain.Skills;

namespace Assets.Sylveed.DDD.Main.Domain.Characters
{
	public class CharacterVm
	{
        readonly CharacterTrait trait;

        ICharacterView view => trait.View;

        public CharacterVmId Id => trait.Id;

		public Vector3 Position { get { return view.Position; } }

		public float Angle { get { return view.Angle; } }

		public CharacterVm(CharacterTrait trait)
		{
            this.trait = trait;
		}

		public void UseSkill(CharacterVmSkillIndex index)
		{
            view.ShowSkill(trait.SkillSet.GetSkill(index));
		}

		public void SetDestinationDirection(Vector3 direction, float speedRatio)
		{
            view.Speed = 10 * speedRatio;
            view.SetDestination(view.Position + direction * 2);
		}

		public void StopMovement()
		{
			view.StopMovement();
		}
	}
}
