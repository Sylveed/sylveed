using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Sylveed.Ido.Domain.Battles;

namespace Sylveed.Ido.UseCases.Battles
{
	public class TurnModel
	{
		readonly Turn turn;

		public BattlerId BattlerId { get { return turn.BattlerId; } }

		public TurnModel(Turn turn)
		{
			this.turn = turn;
		}
	}
}