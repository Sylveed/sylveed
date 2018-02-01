using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.ComponentDI;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Data.Skills;
using Assets.Sylveed.DDD.Main.Implementation.Skills.Robots;
using Assets.Sylveed.DDD.Main.Implementation.Characters.Helpers;

namespace Assets.Sylveed.DDD.Main.Implementation.Characters
{
    public class RobotBody : MonoBehaviour, ICharacterBody
	{
		[DINamedComponent]
		Transform nozzle;
		[DINamedComponent(true)]
		Transform nozzleStub;

		void Awake()
		{
			ComponentResolver.Resolve(this);
		}

		[SkillMethod]
		void Shoot(ShootBulletView view, ISkillTarget[] targets, SkillVm skill)
		{
			view.transform.position = nozzleStub.position;
			view.Shoot(nozzle.forward, 5);
		}
	}
}
