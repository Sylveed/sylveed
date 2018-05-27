using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Sylveed.Ido.Domain.Battles
{
	public class Turn
	{
		public TurnId Id { get; private set; }
		public BattlerId BattlerId { get; private set; }

		public Turn(TurnId id, BattlerId battlerId)
		{
			Id = id;
			BattlerId = battlerId;
		}
	}
}