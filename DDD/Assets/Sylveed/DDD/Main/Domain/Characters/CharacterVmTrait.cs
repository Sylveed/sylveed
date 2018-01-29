using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Main.Application;
using Assets.Sylveed.DDD.Data.Characters;
using Assets.Sylveed.DDD.Data.Skills;
using Assets.Sylveed.DDD.Main.Domain.Players;


namespace Assets.Sylveed.DDD.Main.Domain.Characters
{
    public class CharacterVmTrait
    {
        public CharacterVmId Id { get; }
        public IEnumerable<Skill> Skills { get; }
		public Character Character { get; }
		public IPlayer Player { get; }

		public CharacterVmTrait(CharacterVmId id, Character character, IEnumerable<Skill> skills, IPlayer player)
        {
            Id = id;
			Character = character;
			Skills = skills.ToArray();
			Player = player;
		}
    }
}
