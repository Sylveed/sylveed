using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Sylveed.Ido.Domain.Battles
{
	public class Battle
	{
		readonly Dictionary<BattlerId, Battler> battlers;
		readonly Dictionary<BattlerId, Battler> allies;
		readonly Dictionary<BattlerId, Battler> enemies;

		public IEnumerable<Battler> Allies { get { return allies.Values; } }

		public IEnumerable<Battler> Enemies { get { return enemies.Values; } }

		public void UpdateTurn()
		{
			throw new NotImplementedException();
		}

		public Battler GetAlly(BattlerId id)
		{
			return allies[id];
		}

		public Battler GetEnemy(BattlerId id)
		{
			return enemies[id];
		}

		public Battler GetBattler(BattlerId id)
		{
			return battlers[id];
		}
	}
}