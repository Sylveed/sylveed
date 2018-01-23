using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sylveed.DDD.Presentation.Container;
using Sylveed.DDD.Presentation.Helpers;
using Sylveed.DDD.Presentation.Model;


namespace Sylveed.DDD.Presentation
{
	public class ServiceManager : InternalSingletonBehaviour<ServiceManager>
	{
		public static SPersonService PersonService => Instance.personService;

		public static ItemService ItemService => Instance.itemService;

		public static SkillService SkillService => Instance.skillService;
		
		SPersonService personService;
		ItemService itemService;
		SkillService skillService;

		protected override void OnAwake()
		{
			var configurator = GetComponent<IServiceConfigurator>();

			personService = new SPersonService(configurator.PersonConfig);
			itemService = new ItemService(configurator.ItemConfig);
			skillService = new SkillService(configurator.SkillConfig);
		}
	}
}
