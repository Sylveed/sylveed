using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sylveed.DDD.Presentation.Helpers;

namespace Sylveed.DDD.Presentation
{
	public interface IServiceConfigurator
	{
		IContainerConfiguration PersonConfig { get; }
		IContainerConfiguration SkillConfig { get; }
		IContainerConfiguration ItemConfig { get; }
	}
}
