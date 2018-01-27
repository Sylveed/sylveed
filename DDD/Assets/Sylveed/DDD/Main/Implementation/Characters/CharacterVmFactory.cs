using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Main.Application;
using Assets.Sylveed.DDD.Data.Characters;

namespace Assets.Sylveed.DDD.Main.Implementation.Characters
{
    public class CharacterVmFactory : ICharacterVmFactory
	{
		[Inject]
		readonly CharacterService personService;
		[Inject]
        readonly ResourceProvider resourceProvider;

        public CharacterVm Create(CharacterVmId id, string name)
        {
            var viewPrefab = resourceProvider.ResourceSet.PersonView;
            var view = UnityEngine.Object.Instantiate(viewPrefab);

            var trait = new CharacterTrait(id, name, view, new CharacterVmSkillSet(new SkillVm[0]));

            return new CharacterVm(trait);
        }
    }
}
