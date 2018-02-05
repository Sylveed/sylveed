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

namespace Assets.Sylveed.DDD.Main.Implementation.Characters
{
    public class CharacterView : MonoBehaviour, ICharacterView, IInjectComponent
    {
		[DITypedComponent]
		readonly CharacterController characterController;
		[DITypedComponent]
		readonly NavMeshAgent navMeshAgent;
		[DITypedComponent]
		readonly ICharacterBody body;
		[DITypedComponent]
		readonly ISkillInvoker skillInvoker;
		[Inject]
		readonly SkillVmService skillService;
		[Inject]
		readonly CharacterVm model;

		public CharacterVm Model { get { return model; } }

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

		public bool CanControl => body.CanControl;

		[Inject]
		void Initialize()
		{
			navMeshAgent.updateRotation = false;
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
			skillVm.Invoke(skillInvoker, targets);

			Debug.Log("show skill " + skill);
		}
    }
}
