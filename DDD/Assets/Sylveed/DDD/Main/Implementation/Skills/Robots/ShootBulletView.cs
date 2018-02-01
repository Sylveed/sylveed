using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.ComponentDI;

namespace Assets.Sylveed.DDD.Main.Implementation.Skills.Robots
{
    public class ShootBulletView : MonoBehaviour, ISkillView
    {
		[DITypedComponent]
		readonly new Rigidbody rigidbody;

        public Vector3 Position
        {
            get { return transform.position; }
        }

        public float Angle
        {
            get { return transform.localEulerAngles.y; }
        }

		void Awake()
		{
			ComponentResolver.Resolve(this);
		}

		public void Shoot(Vector3 direction, float speed)
		{
			rigidbody.AddForce(direction.normalized * speed, ForceMode.Impulse);
		}
	}
}
