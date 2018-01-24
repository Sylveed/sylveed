using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Data.SPersons
{
	public class SPersonId : Identity<SPersonId, int>
	{
		public SPersonId(int value) : base(value)
		{
		}
	}
}
