using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Sylveed.Ido.Domain.Battles
{
	public class Battler
	{
		public BattlerId Id { get; private set; }

		public bool IsAlive { get { throw new NotImplementedException(); } }

		public bool IsDead { get { return !IsAlive; } }

		public Battler()
		{

		}

		public void Action(IBattlerCommand command, BattlerCommandDispatchService dispatchService)
		{
			dispatchService.Dispatch(this, command);
		}
	}
}