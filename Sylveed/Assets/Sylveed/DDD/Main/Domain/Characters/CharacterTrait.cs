using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.DDD.Main.Domain.Characters
{
    public class CharacterTrait
    {
        public CharacterVmId Id { get; }
        public string Name { get; }
        public ICharacterView View { get; }
        public CharacterVmSkillSet SkillSet { get; }

        public CharacterTrait(CharacterVmId id, string name, ICharacterView view, CharacterVmSkillSet skillSet)
        {
            Id = id;
            Name = name;
            View = view;
            SkillSet = skillSet;
        }
    }
}
