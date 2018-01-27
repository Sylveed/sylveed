using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Text;
using UniRx;
using UnityEngine;


namespace Assets.Sylveed.DDDTools.Test
{
	static class Sample
	{
        class SampleView
        {
            [Inject]
            readonly SampleService service;

            void Awake()
            {
                ServiceResolver.ResolveMembers(this);

                var model = service.GetWithSubId("test");
            }
        }

        static class ServiceResolver
        {
            static ObjectResolver s_resolver;

            static ServiceResolver()
            {
                var inner = new ObjectResolver()
                    .Register<IModel1Factory>(new SampleModel1Factory())
                    .Register(new SampleRepository());

                s_resolver = new ObjectResolver()
                    .Register(inner.ResolveMembers(new SampleService()));
            }

            public static T ResolveMembers<T>(T target)
            {
                s_resolver.ResolveMembers(target);
                return target;
            }
        }

        class SampleRepository : StorageBase<int, Model1>
        {
            public IStorageIndex<string, Model1> SubIdIndex { get; }

            public SampleRepository() : base(model => model.Id)
            {
                SubIdIndex = CreateIndex(x => x.SubId);
            }
        }

        interface IModel1Factory
        {
            Model1 Create(int id);
        }

        class SampleModel1Factory : IModel1Factory
        {
            public Model1 Create(int id)
            {
                return new Model1();
            }
        }

        class Model1
		{
			public int Id { get; private set; }
			public string SubId { get; private set; }
		}

		class Model2
		{
			public string Id { get; private set; }
		}

		class SampleService
		{
            [Inject]
			readonly SampleRepository repository;
            [Inject]
            readonly IModel1Factory factory;

            [Inject]
            void Initialze()
            {

            }

            public Model1 Get(int id)
			{
				return repository.Get(id);
			}

			public IEnumerable<Model1> GetWithSubId(string subId)
			{
                return repository.SubIdIndex.Get(subId);
            }

			public Model1 Create(int id)
			{
                return repository.Add(factory.Create(id));
			}
		}
	}
}
