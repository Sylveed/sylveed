using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sylveed.DDD.Presentation.Helpers;
using Sylveed.DDD.Presentation.Model;
using UniRx;


namespace Sylveed.DDD.Presentation.Container
{
	public class SkillService
	{
		readonly Factory<IObservable<Skill[]>, Unit> factory;
		readonly RepositoryBase<SkillId, Skill> repository;

		public SkillService(IContainerConfiguration config)
		{
			config.Configure(this);
		}

		public IObservable<Unit> Load()
		{
			return factory.Create(Unit.Default)
				.Do(skills =>
				{
					repository.Clear();

					foreach (var skill in skills)
					{
						repository.Add(skill);
					}
				})
				.AsUnitObservable();
		}
	}
}
