using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Main.Domain.SPersons;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Main.Application;

namespace Assets.Sylveed.DDD.Main.Implementation.SPersons
{
    public class SPersonFactory : ISPersonFactory
    {
        [Inject]
        readonly ResourceProvider resourceProvider;

        public SPerson Create(SPersonId id, string name)
        {
            var viewPrefab = resourceProvider.ResourceSet.PersonView;
            var view = UnityEngine.Object.Instantiate(viewPrefab);

            var trait = new SPersonTrait(id, name, view, new SPersonSkillSet(new Skill[0]));

            return new SPerson(trait);
        }
    }
}
