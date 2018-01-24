using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Main.Infrastructure;


namespace Assets.Sylveed.DDD.Main.Domain.SPersons
{
	public class SPersonId : Identity<SPersonId, Guid>
	{
		public SPersonId(Guid value) : base(value)
		{
		}
	}
}
