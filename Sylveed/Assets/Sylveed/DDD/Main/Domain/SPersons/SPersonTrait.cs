using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.DDD.Main.Domain.SPersons
{
    public class SPersonTrait
    {
        public SPersonVmId Id { get; }
        public string Name { get; }
        public ISPersonView View { get; }
        public SPersonVmSkillSet SkillSet { get; }

        public SPersonTrait(SPersonVmId id, string name, ISPersonView view, SPersonVmSkillSet skillSet)
        {
            Id = id;
            Name = name;
            View = view;
            SkillSet = skillSet;
        }
    }
}
