using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Main.Domain.SPersons;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Main.Application;
using Assets.Sylveed.DDD.Data.SPersons;

namespace Assets.Sylveed.DDD.Main.Implementation.SPersons
{
    public class SPersonVmFactory : ISPersonVmFactory
	{
		[Inject]
		readonly SPersonService personService;
		[Inject]
        readonly ResourceProvider resourceProvider;

        public SPersonVm Create(SPersonVmId id, string name)
        {
            var viewPrefab = resourceProvider.ResourceSet.PersonView;
            var view = UnityEngine.Object.Instantiate(viewPrefab);

            var trait = new SPersonTrait(id, name, view, new SPersonVmSkillSet(new SkillVm[0]));

            return new SPersonVm(trait);
        }
    }
}
