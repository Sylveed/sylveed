using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Application;
using Assets.Sylveed.DDD.Data;
using Assets.Sylveed.DDD.Data.Items;
using Assets.Sylveed.DDD.Data.Skills;
using Assets.Sylveed.DDD.Data.SPersons;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Main.Domain.Items;
using Assets.Sylveed.DDD.Main.Domain.SPersons;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Main.Application;
using Assets.Sylveed.DDD.Main.Implementation.SPersons;
using Assets.Sylveed.DDD.Main.Implementation.Skills;
using Assets.Sylveed.DDD.Main.Implementation.Items;

namespace Assets.Sylveed.DDD.Main
{
    public class ServiceResolver : AutoInternalSingletonBehaviour<ServiceResolver>
    {
		ObjectResolver serviceResolver;

        protected override void OnAwake()
        {
			var globalServiceResolver = GlobalServiceResolver.GetServiceResolver();

            var baseResolver = new ObjectResolver()
                .InheritFrom<SkillService>(globalServiceResolver)
				.InheritFrom<ItemService>(globalServiceResolver)
				.InheritFrom<SPersonService>(globalServiceResolver)
				.Register(new ResourceProvider());

            var inner = new ObjectResolver()
                .Register<IItemVmFactory>(new ItemVmFactory())
                .Register<ISkillVmFactory>(new SkillVmFactory())
                .Register<ISPersonVmFactory>(new SPersonVmFactory())
                .Register(new ItemVmStorage())
                .Register(new SkillVmStorage())
                .Register(new SPersonVmStorage())
				.DependOn(baseResolver);

            serviceResolver = new ObjectResolver()
                .Register(new ItemVmService())
                .Register(new SkillVmService())
                .Register(new SPersonVmService())
				.DependOn(inner);
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

		public static T Resolve<T>(out T @object)
		{
			@object = Instance.serviceResolver.Resolve<T>();
			return @object;
		}
	}
}
