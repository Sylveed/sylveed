using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using Assets.Sylveed.DDD.Application;
using Assets.Sylveed.DDD.Data;
using Assets.Sylveed.DDD.Data.Items;
using Assets.Sylveed.DDD.Data.Skills;
using Assets.Sylveed.DDD.Data.Characters;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Main.Domain.Items;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Main.Domain.Players;
using Assets.Sylveed.DDD.Main.Application;
using Assets.Sylveed.DDD.Main.Implementation.Characters;
using Assets.Sylveed.DDD.Main.Implementation.Skills;
using Assets.Sylveed.DDD.Main.Implementation.Items;
using Assets.Sylveed.DDD.Main.Implementation.Helpers;

namespace Assets.Sylveed.DDD.Main
{
	public class DataTypeValidator
	{
		[Inject]
		readonly CharacterService characterService;
		[Inject]
		readonly SkillService skillService;
		
		public void Validate()
		{
			foreach(var character in characterService.Items)
			{
				var characterViewType = DataTypeConfiguration.GetEntry(character.Id);
				
				foreach(var skillId in characterService.GetSkillIds(character.Id))
				{
					skillService.Get(skillId);

					var skillViewType = DataTypeConfiguration.GetEntry(skillId);

					SkillRouter.Validate(characterViewType, skillViewType);
				}
			}
		}
	}
}
