using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Data.Characters
{
	public class CharacterId : Identity<CharacterId, int>
	{
		public CharacterId(int value) : base(value)
		{
		}
	}
}
