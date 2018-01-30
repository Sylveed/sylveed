using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Data.Characters
{
	public class CharacterFamilyId : Identity<CharacterFamilyId, int>
	{
		public CharacterFamilyId(int value) : base(value)
		{
		}
	}
}
