using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Domain.SPersons;
using Assets.Sylveed.DDD.Main.Domain.Skills;

namespace Assets.Sylveed.DDD.Main.Implementation.SPersons
{
    public class SPersonView : MonoBehaviour, ISPersonView
    {
		CharacterController characterController;
		float speed = 3f;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
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
			characterController = GetComponent<CharacterController>();
		}

        public void MoveTo(Vector3 direction)
        {
			direction.y = 0;
			//transform.Translate(direction.normalized * speed * Time.deltaTime);
			characterController.SimpleMove(direction.normalized * speed);
		}

        public void ShowSkill(SkillVm skill)
		{
			Debug.Log("show skill " + skill);
		}
    }
}
