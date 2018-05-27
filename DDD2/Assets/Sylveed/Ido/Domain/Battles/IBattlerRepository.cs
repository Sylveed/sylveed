using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Sylveed.Ido.Domain.Battles
{
	public interface IBattlerRepository
	{
		IEnumerable<Battler> GetBattlers();
		IEnumerable<Battler> GetAllies();
		IEnumerable<Battler> GetEnemies();

		Battler Get(BattlerId id);
		void AddAlly(Battler battler);
		void AddEnemy(Battler battler);
		void Delete(BattlerId id);
	}
}