using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;


namespace Assets.Sylveed.DDD.Main.Domain.SPersons
{
	public class SPersonService
    {
        [Inject]
        readonly ISPersonFactory factory;
        [Inject]
        readonly SPersonRepository repository;

		SPersonId playerId;

		public SPerson Player
		{
			get { return playerId == null ? null : repository.Get(playerId); }
		}

		public SPerson Create(string name)
		{
			return repository.Add(factory.Create(new SPersonId(), name));
		}

		public void SetPlayer(SPersonId playerId)
		{
			this.playerId = playerId;
		}
	}
}
