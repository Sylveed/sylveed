using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Sylveed.DDD.Data.Characters
{
	public class Character
	{
		public CharacterId Id { get; }
		public CharacterFamilyId FamilyId { get; }
        public string Name { get; }

        public Character(CharacterId id, CharacterFamilyId familyId, string name)
		{
            Id = id;
			FamilyId = familyId;
			Name = name;
		}
	}
}
