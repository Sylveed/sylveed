using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.ComponentDI;
using Assets.Sylveed.DDD.Main.Domain.Characters;

namespace Assets.Sylveed.DDD.Main.Implementation.Characters
{
    public class RobotBody : MonoBehaviour, IRobotBody
	{
		[DINamedComponent]
		Transform nozzle;
		[DINamedComponent(true)]
		Transform nozzleStub;

		public Vector3 NozzleDirection
		{
			get { return nozzle.forward; }
		}

		public Vector3 NozzleStubPosition
		{
			get { return nozzleStub.position; }
		}

		void Awake()
		{
			ComponentResolver.Resolve(this);
		}
	}
}
