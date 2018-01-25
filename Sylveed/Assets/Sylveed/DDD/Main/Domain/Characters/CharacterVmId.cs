using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;


namespace Assets.Sylveed.DDD.Main.Domain.Characters
{
	public class CharacterVmId : Identity<CharacterVmId, Guid>
	{
        public CharacterVmId() : this(Guid.NewGuid()) { }

		public CharacterVmId(Guid value) : base(value)
		{
		}
	}
}
