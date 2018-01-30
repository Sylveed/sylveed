using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Application;
using Assets.Sylveed.DDD.Data.Items;
using Assets.Sylveed.DDD.Data.Skills;
using Assets.Sylveed.DDD.Data.Characters;

namespace Assets.Sylveed.DDD.Application
{
    public class GlobalServiceResolver : AutoInternalSingletonBehaviour<GlobalServiceResolver>
    {
        ObjectResolver serviceResolver;

        protected override void OnAwake()
        {
			var componentResolver = new ObjectResolver()
				.Register(new ItemStorage())
				.Register(new SkillStorage())
				.Register(new CharacterStorage())
				.Register(new CharacterSkillStorage());

			serviceResolver = new ObjectResolver()
                .Register(new ItemService())
				.Register(new SkillService())
				.Register(new CharacterService())
				.DependOn(componentResolver);

			TestSetup(componentResolver);
		}

        public static T ResolveMembers<T>(T target)
        {
            Instance.serviceResolver.ResolveMembers(target);
            return target;
		}

		public static T Resolve<T>()
		{
			return Instance.serviceResolver.Resolve<T>();
		}

		public static ObjectResolver GetServiceResolver()
		{
			return Instance.serviceResolver;
		}

		static void TestSetup(ObjectResolver componentResolver)
		{
			TestSetupStorage(componentResolver.Resolve<ItemStorage>());
			TestSetupStorage(componentResolver.Resolve<SkillStorage>());
			TestSetupStorage(componentResolver.Resolve<CharacterStorage>());
			TestSetupStorage(componentResolver.Resolve<CharacterSkillStorage>());
		}

		static void TestSetupStorage(ItemStorage storage)
		{
			storage.Add(new Item(new ItemId(1), "Item1", ItemType.Recovery));
			storage.Add(new Item(new ItemId(2), "Item2", ItemType.Recovery));
			storage.Add(new Item(new ItemId(3), "Item3", ItemType.Recovery));
		}

		static void TestSetupStorage(SkillStorage storage)
		{
			storage.Add(new Skill(new SkillId(1), "Skill1"));
			storage.Add(new Skill(new SkillId(2), "Skill2"));
			storage.Add(new Skill(new SkillId(3), "Skill3"));
		}

		static void TestSetupStorage(CharacterStorage storage)
		{
			storage.Add(new Character(new CharacterId(1), new CharacterFamilyId(1), "Character1"));
			storage.Add(new Character(new CharacterId(2), new CharacterFamilyId(1), "Character2"));
			storage.Add(new Character(new CharacterId(3), new CharacterFamilyId(1), "Character3"));
		}

		static void TestSetupStorage(CharacterSkillStorage storage)
		{
			storage.Add(
				new CharacterSkill(
					new CharacterSkillId(
						new CharacterId(1),
						new SkillId(1)
						)
					)
				);

			storage.Add(
				new CharacterSkill(
					new CharacterSkillId(
						new CharacterId(1),
						new SkillId(2)
						)
					)
				);

			storage.Add(
				new CharacterSkill(
					new CharacterSkillId(
						new CharacterId(1),
						new SkillId(3)
						)
					)
				);
		}
	}
}
