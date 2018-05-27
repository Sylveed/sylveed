using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Sylveed.Ido.Domain.Battles;

namespace Sylveed.Ido.UseCases.Battles
{
	public class BattleUseCase
	{
		readonly IBattlerRepository battlerRepository;
		readonly IBattlerOperatorRepository battlerOperatorRepository;
		readonly ITurnRepository turnRepository;
		readonly TurnProgressService turnProgressService;

		public void DispatchBattlerAction(BattlerId id)
		{
			var battler = battlerRepository.Get(id);
			var @operator = battlerOperatorRepository.Get(id);
			var command = @operator.DetermineCommand();

			battler.Action(command, new BattlerCommandDispatchService(battlerRepository));
		}

		public IEnumerable<BattlerModel> GetAllies()
		{
			return battlerRepository.GetAllies().Select(x => new BattlerModel(x));
		}

		public IEnumerable<BattlerModel> GetEnemies()
		{
			return battlerRepository.GetEnemies().Select(x => new BattlerModel(x));
		}

		public TurnModel ProgressTurn()
		{
			turnProgressService.Progress();

			var turn = turnRepository.GetCurrent();

			return new TurnModel(turn);
		}
	}
}