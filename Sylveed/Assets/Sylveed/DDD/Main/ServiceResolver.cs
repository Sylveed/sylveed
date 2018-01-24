using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Main.Domain.Items;
using Assets.Sylveed.DDD.Main.Domain.SPersons;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Main.Application;
using Assets.Sylveed.DDD.Main.Implementation.SPersons;

namespace Assets.Sylveed.DDD.Main
{
    public static class ServiceResolver
    {
        static ObjectResolver s_resolver;

        static ServiceResolver()
        {
            var baseResolver = new ObjectResolver()
                .Register(new ResourceProvider());

            var inner = new ObjectResolver()
                .Register<IItemFactory>(baseResolver.ResolveMembers<IItemFactory>(null))
                .Register<ISkillFactory>(baseResolver.ResolveMembers<ISkillFactory>(null))
                .Register<ISPersonFactory>(baseResolver.ResolveMembers(new SPersonFactory()))
                .Register(new ItemRepository())
                .Register(new SkillRepository())
                .Register(new SPersonRepository());

            s_resolver = new ObjectResolver()
                .Register(inner.ResolveMembers(new ItemService()))
                .Register(inner.ResolveMembers(new SkillService()))
                .Register(inner.ResolveMembers(new SPersonService()));
        }

        public static T ResolveMembers<T>(T target)
        {
            s_resolver.ResolveMembers(target);
            return target;
        }
    }
}
