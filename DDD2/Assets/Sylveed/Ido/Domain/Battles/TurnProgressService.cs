using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Sylveed.Ido.Domain.Battles
{
	public class TurnProgressService
	{
		readonly ITurnRepository repository;
		readonly IBattlerRepository battlerRepository;

		public void Progress()
		{
			var current = repository.GetCurrent();
			repository.Delete(current.Id);

			var last = DetermineNewTern();

			repository.AddLast(last);
		}

		Turn DetermineNewTern()
		{
			var battlers = battlerRepository.GetBattlers();
			var turns = repository.GetTurns().ToArray();

			var notRegisteredBattlerIds = battlers
				.Select(x => x.Id)
				.Except(turns.Select(x => x.BattlerId))
				.ToArray();

			var id = new TurnId();

			if (notRegisteredBattlerIds.Length > 0)
			{
				var randomIndex = UnityEngine.Random.Range(0, notRegisteredBattlerIds.Length);
				var battlerId = notRegisteredBattlerIds[randomIndex];

				return new Turn(id, battlerId);
			}
			else
			{
				var randomIndex = UnityEngine.Random.Range(0, turns.Length);
				var battlerId = turns[randomIndex].BattlerId;

				return new Turn(id, battlerId);
			}
		}
	}
}