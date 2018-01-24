using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;


namespace Assets.Sylveed.DDD.Main.Domain.SPersons
{
	public class SPersonVmId : Identity<SPersonVmId, Guid>
	{
        public SPersonVmId() : this(Guid.NewGuid()) { }

		public SPersonVmId(Guid value) : base(value)
		{
		}
	}
}
