using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Implementation.Characters;
using Assets.Sylveed.DDD.Main.Implementation.SkillDerivations;

namespace Assets.Sylveed.DDD.Main.Application
{
    public class ResourceSet : ScriptableObject
    {
        [SerializeField]
        RobotView personView;
		[SerializeField]
		SkillSet skills;

		public RobotView PersonView => personView;

		public SkillSet Skills => skills;

		public static ResourceSet Load()
        {
            return (ResourceSet)Resources.Load("Sylveed/DDD/Main/ResourceSet");
        }

		[Serializable]
		public class SkillSet
		{
			[SerializeField]
			ShootBulletView shootBulletView;

			public ShootBulletView ShootBulletView => shootBulletView;
		}
    }
}
