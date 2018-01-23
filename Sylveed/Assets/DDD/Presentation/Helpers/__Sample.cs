using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Text;
using UniRx;
using UnityEngine;


namespace Sylveed.DDD.Presentation.Helpers
{
	static class Sample
	{
		static void Main()
		{
			var repository1 = new RepositoryBase<int, Model1>(x => x.Id);
			var repository2 = new RepositoryBase<string, Model2>(x => x.Id);
			
			var container = new Container(new ContainerConfiguration()
				.AddFactory<Model1>()
				.Add((int i) => new Model1())
				.Add((string s) => new Model1())
				.Map()
				.AddRepository(repository1)
				.AddRepository(repository2));
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

		class Container
		{
			readonly RepositoryBase<int, Model1> repository1;
			readonly RepositoryBase<string, Model2> repository2;
			readonly Factory<Model1, int> factory1;
			readonly Factory<Model1, string> factory2;

			readonly IRepositoryIndexer<string, Model1> subIdModel1Indexer;

			public Container(IContainerConfiguration config)
			{
				config.Configure(this);

				subIdModel1Indexer = repository1.Index(x => x.SubId);
			}

			public Model1 Get(int id)
			{
				return repository1.Get(id);
			}

			public Model1 GetWithSubId(string subId)
			{
				return subIdModel1Indexer.Get(subId);
			}

			public Model1 Create(int id)
			{
				var obj = factory1.Create(id);

				repository1.Add(obj);

				return obj;
			}
		}
	}
}
