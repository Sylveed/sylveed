using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Application;
using Assets.Sylveed.DDD.Data.Items;
using Assets.Sylveed.DDD.Data.Skills;
using Assets.Sylveed.DDD.Data.SPersons;

namespace Assets.Sylveed.DDD.Main
{
    public class GlobalServiceResolver : AutoInternalSingletonBehaviour<GlobalServiceResolver>
    {
        ObjectResolver serviceResolver;

        protected override void OnAwake()
        {
			var componentResolver = new ObjectResolver()
				.Register(new ItemStorage())
				.Register(new SkillStorage())
				.Register(new SPersonStorage());

			serviceResolver = new ObjectResolver()
                .Register(componentResolver.ResolveMembers(new ItemService()))
				.Register(componentResolver.ResolveMembers(new SkillService()))
				.Register(componentResolver.ResolveMembers(new SPersonService()));
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
	}
}
