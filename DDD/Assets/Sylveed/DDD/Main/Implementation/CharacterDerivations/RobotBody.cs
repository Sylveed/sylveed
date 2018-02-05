using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.ComponentDI;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Main.Implementation.Helpers;
using Assets.Sylveed.DDD.Main.Implementation.SkillDerivations;
using Assets.Sylveed.DDD.Data.Skills;
using UniRx;

namespace Assets.Sylveed.DDD.Main.Implementation.CharacterDerivations
{
    public class RobotBody : MonoBehaviour, ICharacterBody, ISkillInvoker, IInjectComponent
	{
		[DINamedComponent]
		Transform nozzle;
		[DINamedComponent(true)]
		Transform nozzleStub;
		[Inject]
		CharacterVm model;

		public bool CanControl { get; private set; }

		[Inject]
		void Initialize()
		{
			CanControl = true;
		}

		public void InvokeSkill(SkillVm skill, ISkillView skillView, ISkillTarget[] targets)
		{
			if (!CanControl)
				throw new InvalidOperationException();

			SkillRouter.Route(this, skill, skillView, targets);
		}

		[SkillMethod]
		void Shoot(ShootBulletView view, ISkillTarget[] targets, SkillVm skill)
		{
			model.StopMovement();

			CanControl = false;

			Observable.ReturnUnit()
				.Delay(TimeSpan.FromSeconds(0.5f))
				.ObserveOnMainThread()
				.Subscribe(_ => CanControl = true);

			{
				var cv = GetComponentInParent<ICharacterView>() as Component;
				var targetPos = cv.transform.forward;
				if (targets.Length > 0)
					targetPos = targets[0].Position;
				cv.transform.LookAt(new Vector3(targetPos.x, cv.transform.position.y, targetPos.z));
			}
			view.transform.position = nozzleStub.position;
			view.Shoot(nozzle.forward, 5);

			view.CollidedWithCharacter.Subscribe(characterView =>
			{
				var otherModel = characterView.Model;

				Debug.Log("hit " + model.Player.Name + "'s bullet to " + otherModel.Player.Name);
			});
		}
	}
}
