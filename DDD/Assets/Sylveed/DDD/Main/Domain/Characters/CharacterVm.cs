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

        ICharacterView view => trait.View;

        public CharacterVmId Id => trait.Id;

		public IPlayer Player => trait.Player;

		public Vector3 Position { get { return view.Position; } }

		public float Angle { get { return view.Angle; } }

		public CharacterVm(CharacterVmTrait trait)
		{
            this.trait = trait;
		}

		public void UseSkill(int index)
		{
			var skill = trait.Skills.ElementAt(index);

			//view.ShowSkill();
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
