using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Main.Infrastructure;
using Assets.Sylveed.DDD.Main.Domain.Items;
using Assets.Sylveed.DDD.Main.Domain.SPersons;
using Assets.Sylveed.DDD.Main.Domain.Skills;

namespace Assets.Sylveed.DDD.Main
{
    public static class ServiceResolver
    {
        static ObjectResolver s_resolver;

        static ServiceResolver()
        {
            var inner = new ObjectResolver()
                .Register<IItemFactory>(null)
                .Register<ISkillFactory>(null)
                .Register<ISPersonFactory>(null)
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
