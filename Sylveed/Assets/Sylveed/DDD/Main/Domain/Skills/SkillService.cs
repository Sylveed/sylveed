using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Main.Infrastructure;
using UniRx;


namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
	public class SkillService
	{
        [Inject]
		readonly ISkillFactory factory;
        [Inject]
        readonly SkillRepository repository;

		public IObservable<Unit> Load()
		{
			return factory.Load()
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
