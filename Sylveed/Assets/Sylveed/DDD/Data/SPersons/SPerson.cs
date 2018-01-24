using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Sylveed.DDD.Data.SPersons
{
	public class SPerson
	{
		public SPersonId Id { get; }
        public string Name { get; }

        public SPerson(SPersonId id, string name)
		{
            Id = id;
            Name = name;
		}
	}
}
