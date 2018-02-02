using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Sylveed.DDDTools;
using Assets.Sylveed.ComponentDI.Internal;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Main.Application;
using Assets.Sylveed.DDD.Data.Characters;
using Assets.Sylveed.DDD.Data.Skills;
using Assets.Sylveed.DDD.Main.Domain.Players;
using Assets.Sylveed.DDD.Main.Implementation.Helpers;

namespace Assets.Sylveed.DDD.Main.Implementation.Characters
{
    public class CharacterVmFactory : ICharacterVmFactory
	{
		[Inject]
		readonly CharacterService characterService;
		[Inject]
		readonly SkillService skillService;
		[Inject]
        readonly ResourceProvider resourceProvider;

        public CharacterVm Create(CharacterVmId id, IPlayer player)
        {
            var viewPrefab = resourceProvider.ResourceSet.PersonView;
            var view = UnityEngine.Object.Instantiate(viewPrefab);

			var characterId = player.CharacterId;
			var character = characterService.Get(characterId);
			var skillIds = characterService.GetSkillIds(characterId);
			var skills = skillIds.Select(x => skillService.Get(x));

			var trait = new CharacterVmTrait(id, character, skills, player);

            var viewModel = new CharacterVm(trait, view);

			ViewInjector.Inject(view, viewModel);

			return viewModel;
		}
    }
}
