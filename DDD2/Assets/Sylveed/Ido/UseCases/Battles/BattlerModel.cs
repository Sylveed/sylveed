using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Sylveed.Ido.Domain.Battles;

namespace Sylveed.Ido.UseCases.Battles
{
	public class BattlerModel
	{
		readonly Battler battler;

		public BattlerId Id { get { return battler.Id; } }

		public BattlerModel(Battler battler)
		{
			this.battler = battler;
		}
	}
}