using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Data.Characters
{
	public class CharacterStorage : StorageBase<CharacterId, Character>
	{
		public CharacterStorage() : base(x => x.Id)
		{

		}
	}
}
