using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;


namespace Assets.Sylveed.DDD.Main.Domain.SPersons
{
	public class SPersonVmService
    {
        [Inject]
        readonly ISPersonVmFactory factory;
        [Inject]
        readonly SPersonVmStorage storage;

		SPersonVmId playerId;

		public SPersonVm Player
		{
			get { return playerId == null ? null : storage.Get(playerId); }
		}

		public SPersonVm Create(string name)
		{
			return storage.Add(factory.Create(new SPersonVmId(), name));
		}

		public void SetPlayer(SPersonVmId playerId)
		{
			this.playerId = playerId;
		}
	}
}
