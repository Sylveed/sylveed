using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Main.Domain.Players;

namespace Assets.Sylveed.DDD.Main.Domain.Characters
{
	public class CharacterVm
	{
        readonly CharacterVmTrait trait;
		readonly ICharacterView view;

        public CharacterVmId Id => trait.Id;

		public IPlayer Player => trait.Player;

		public Vector3 Position { get { return view.Position; } }

		public float Angle { get { return view.Angle; } }

		public bool IsDisposed => view == null;

		public bool CanControl => view.CanControl;

		public CharacterVm(CharacterVmTrait trait, ICharacterView view)
		{
            this.trait = trait;
			this.view = view;
		}

		public void UseSkill(int index, params ISkillTarget[] targets)
		{
			if (!CanControl) return;

			var skill = trait.Skills.ElementAt(index);

			view.ShowSkill(skill, targets);
		}

		public void SetDestinationDirection(Vector3 direction, float speedRatio)
		{
			if (!CanControl) return;

			view.Speed = 10 * speedRatio;
            view.SetDestination(view.Position + direction * 2);
		}

		public void StopMovement()
		{
			if (!CanControl) return;

			view.StopMovement();
		}
	}
}
