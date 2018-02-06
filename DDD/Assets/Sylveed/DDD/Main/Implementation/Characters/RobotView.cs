using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.ComponentDI;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Data.Skills;
using Assets.Sylveed.DDD.Main.Implementation.Helpers;
using Assets.Sylveed.DDD.Main.Implementation.SkillDerivations;
using UniRx;

namespace Assets.Sylveed.DDD.Main.Implementation.Characters
{
    public class RobotView : SimpleCharacterViewBase<RobotView>
	{
		[DINamedComponent(true)]
		Transform nozzle;
		[DINamedComponent(true)]
		Transform nozzleStub;

		[SkillMethod]
		void Shoot(ShootBulletView view, ISkillTarget[] targets, SkillVm skill)
		{
			StopMovement();

			CanControl = false;

			Observable.ReturnUnit()
				.Delay(TimeSpan.FromSeconds(0.5f))
				.ObserveOnMainThread()
				.Subscribe(_ => CanControl = true);

			{
				var targetPos = transform.position + transform.forward;

				if (targets.Length > 0)
				{
					targetPos = targets[0].Position;
				}

				transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));
			}
			view.transform.position = nozzleStub.position;
			view.Shoot(nozzle.forward, 5);

			view.CollidedWithCharacter.Subscribe(characterView =>
			{
				var otherModel = characterView.Model;

				Debug.Log("hit " + Model.Player.Name + "'s bullet to " + otherModel.Player.Name);
			});
		}
	}
}
