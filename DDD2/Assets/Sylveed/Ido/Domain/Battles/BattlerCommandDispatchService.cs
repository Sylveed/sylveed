using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Sylveed.Ido.Domain.Battles
{
	public class BattlerCommandDispatchService
	{
		readonly IBattlerRepository battlerRepository;

		public BattlerCommandDispatchService(IBattlerRepository battlerRepository)
		{
			this.battlerRepository = battlerRepository;
		}

		public void Dispatch(Battler battler, IBattlerCommand command)
		{
			throw new NotImplementedException();
		}
	}
}