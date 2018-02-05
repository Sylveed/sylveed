using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Data.Skills;

namespace Assets.Sylveed.DDD.Main.Domain.Characters
{
    public interface ICharacterView
    {
        float Speed { get; set; }
        void SetDestination(Vector3 direction);
		void StopMovement();
        void ShowSkill(Skill skill, ISkillTarget[] targets);
        Vector3 Position { get; }
        float Angle { get; }
		CharacterVm Model { get; }
		bool CanControl { get; }
    }
}
