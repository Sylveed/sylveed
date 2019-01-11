using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sylveed.SampleApp
{
	public class Skill
	{
		public SkillId Id { get; private set; }

		public Skill(SkillId id)
		{
			Id = id;
		}
	}

}