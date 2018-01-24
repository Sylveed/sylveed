using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;


namespace Assets.Sylveed.DDD.Main.Domain.SPersons
{
	public class SPersonId : Identity<SPersonId, Guid>
	{
        public SPersonId() : this(Guid.NewGuid()) { }

		public SPersonId(Guid value) : base(value)
		{
		}
	}
}
