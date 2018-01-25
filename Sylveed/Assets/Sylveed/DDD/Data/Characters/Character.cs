using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Sylveed.DDD.Data.Characters
{
	public class Character
	{
		public CharacterId Id { get; }
        public string Name { get; }

        public Character(CharacterId id, string name)
		{
            Id = id;
            Name = name;
		}
	}
}
