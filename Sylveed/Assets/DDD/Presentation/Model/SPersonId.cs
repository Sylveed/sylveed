using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sylveed.DDD.Presentation.Helpers;


namespace Sylveed.DDD.Presentation.Model
{
	public class SPersonId : Identity<SPersonId, Guid>
	{
		public SPersonId(Guid value) : base(value)
		{
		}
	}
}
