using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.DDD.Main.Domain.SPersons
{
    public class SPersonTrait
    {
        public SPersonId Id { get; }
        public string Name { get; }
        public ISPersonView View { get; }
        public SPersonSkillSet SkillSet { get; }

        public SPersonTrait(SPersonId id, string name, ISPersonView view, SPersonSkillSet skillSet)
        {
            Id = id;
            Name = name;
            View = view;
            SkillSet = skillSet;
        }
    }
}
