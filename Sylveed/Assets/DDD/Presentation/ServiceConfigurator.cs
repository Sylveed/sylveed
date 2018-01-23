using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sylveed.DDD.Presentation.Helpers;
using Sylveed.DDD.Presentation.Model;
using UnityEngine;

namespace Sylveed.DDD.Presentation
{
	public class ServiceConfigurator : MonoBehaviour, IServiceConfigurator
	{
		readonly RepositoryBase<SPersonId, SPerson> personRepository = new RepositoryBase<SPersonId, SPerson>(x => x.Id);
		readonly RepositoryBase<SkillId, Skill> skillRepository = new RepositoryBase<SkillId, Skill>(x => x.Id);
		readonly RepositoryBase<ItemId, Item> itemRepository = new RepositoryBase<ItemId, Item>(x => x.Id);

		public IContainerConfiguration ItemConfig
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IContainerConfiguration PersonConfig
		{
			get
			{
				return new ContainerConfiguration()
					.AddFactory<SPerson>()
					.Add<Tuple<SPersonId, string>>(arg =>
					{
						return new SPerson(new SPerson.Parameter(
							arg.Item1,
							arg.Item2,
							null,
							null));
					})
					.Map()
					.AddRepository(personRepository);
			}
		}

		public IContainerConfiguration SkillConfig
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		bool hasInit = false;

		void Awake()
		{
			if (hasInit) return;
			hasInit = true;
		}
	}
}
