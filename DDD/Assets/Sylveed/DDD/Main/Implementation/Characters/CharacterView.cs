using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.ComponentDI;

namespace Assets.Sylveed.DDD.Main.Implementation.Characters
{
    public class CharacterView : MonoBehaviour, ICharacterView
    {
		[DITypedComponent]
		CharacterController characterController;
		[DITypedComponent]
		NavMeshAgent navMeshAgent;

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

		private void Awake()
		{
			ComponentResolver.Resolve(this);

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

        public void ShowSkill(SkillVm skill)
		{
			Debug.Log("show skill " + skill);
		}
    }
}
