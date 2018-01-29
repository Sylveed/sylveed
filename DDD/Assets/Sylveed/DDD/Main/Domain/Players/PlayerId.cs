using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;


namespace Assets.Sylveed.DDD.Main.Domain.Players
{
	public class PlayerId : Identity<PlayerId, Guid>
	{
        public PlayerId() : this(Guid.NewGuid()) { }

		public PlayerId(Guid value) : base(value)
		{
		}
	}
}
