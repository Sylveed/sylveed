using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Data.SPersons
{
	public class SPersonStorage : StorageBase<SPersonId, SPerson>
	{
		public SPersonStorage() : base(x => x.Id)
		{

		}
	}
}
