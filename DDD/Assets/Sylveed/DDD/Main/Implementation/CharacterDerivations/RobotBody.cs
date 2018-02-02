using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.ComponentDI;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Main.Implementation.Helpers;
using Assets.Sylveed.DDD.Main.Implementation.SkillDerivations;
using Assets.Sylveed.DDD.Data.Skills;

namespace Assets.Sylveed.DDD.Main.Implementation.CharacterDerivations
{
    public class RobotBody : MonoBehaviour, ICharacterBody, ISkillInvoker, IInjectComponent
	{
		[DINamedComponent]
		Transform nozzle;
		[DINamedComponent(true)]
		Transform nozzleStub;

		public void InvokeSkill(SkillVm skill, ISkillView skillView, ISkillTarget[] targets)
		{
			SkillRouter.Route(this, skill, skillView, targets);
		}

		[SkillMethod]
		void Shoot(ShootBulletView view, ISkillTarget[] targets, SkillVm skill)
		{
			view.transform.position = nozzleStub.position;
			view.Shoot(nozzle.forward, 5);
		}
	}
}
