using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;
using UniRx;


namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
	public class SkillVmService
	{
        [Inject]
		readonly ISkillVmFactory factory;
        [Inject]
        readonly SkillVmStorage storage;

		public IObservable<Unit> Load()
		{
			return factory.Load()
				.Do(skills =>
				{
					storage.Clear();

					foreach (var skill in skills)
					{
						storage.Add(skill);
					}
				})
				.AsUnitObservable();
		}
	}
}
