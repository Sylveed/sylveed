using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sylveed.DDD.Presentation.Helpers;
using Sylveed.DDD.Presentation.Model;


namespace Sylveed.DDD.Presentation.Container
{
	public class SPersonService
	{
		readonly Factory<SPerson, Tuple<SPersonId, string>> factory;
		readonly RepositoryBase<SPersonId, SPerson> repository;

		SPersonId playerId;

		public SPerson Player
		{
			get { return playerId == null ? null : repository.Get(playerId); }
		}

		public SPersonService(IContainerConfiguration config)
		{
			config.Configure(this);
		}

		public SPerson Create(string name)
		{
			var id = new SPersonId(Guid.NewGuid());
			
			return repository.Add(factory.Create(Tuple.Create(id, name)));
		}

		public void SetPlayer(SPersonId playerId)
		{
			this.playerId = playerId;
		}
	}
}
