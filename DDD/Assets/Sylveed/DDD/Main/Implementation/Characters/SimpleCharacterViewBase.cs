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
	public abstract class SimpleCharacterViewBase<T> : MonoBehaviour, ICharacterView, IInjectComponent, ISkillInvoker
		where T : SimpleCharacterViewBase<T>
	{
		[DITypedComponent]
		readonly CharacterController characterController;
		[DITypedComponent]
		readonly NavMeshAgent navMeshAgent;
		[Inject]
		readonly SkillVmService skillService;
		[Inject]
		readonly CharacterVm model;

		protected CharacterController CharacterController => characterController;

		protected NavMeshAgent NavMeshAgent => navMeshAgent;

		protected SkillVmService SkillService => skillService;

		public CharacterVm Model => model;

		public bool CanControl { get; protected set; }

		public float Speed
        {
            get { return navMeshAgent.speed; }
            set { navMeshAgent.speed = value; }
        }

        public Vector3 Position
        {
            get { return transform.position; }
        }

        public float Angle
        {
            get { return transform.localEulerAngles.y; }
		}

		[Inject]
		void Initialize()
		{
			navMeshAgent.updateRotation = false;
			CanControl = true;
		}

		public void InvokeSkill(SkillVm skill, ISkillView skillView, ISkillTarget[] targets)
		{
			if (!CanControl)
				throw new InvalidOperationException();

			SkillRouter.Route((T)this, skill, skillView, targets);
		}

		public void SetDestination(Vector3 destination)
        {
			navMeshAgent.SetDestination(destination);
			transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
		}

		public void StopMovement()
		{
			navMeshAgent.SetDestination(transform.position);
		}

        public void ShowSkill(Skill skill, ISkillTarget[] targets)
		{
			var skillVm = skillService.Create(skill.Id);
			skillVm.Invoke(this, targets);

			Debug.Log("show skill " + skill);
		}
	}
}
