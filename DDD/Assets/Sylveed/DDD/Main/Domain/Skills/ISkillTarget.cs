using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
	public interface ISkillTarget
	{
		Vector3 Position { get; }
	}

	public static class SkillTarget
	{
		public static ISkillTarget Create(Vector3 position)
		{
			return new AnonymousSkillTarget(position);
		}

		class AnonymousSkillTarget : ISkillTarget
		{
			public Vector3 Position { get; }

			public AnonymousSkillTarget(Vector3 position)
			{
				Position = position;
			}
		}
	}
}
