using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Sylveed.Ido.Domain.Battles
{
	public interface ITurnRepository
	{
		IEnumerable<Turn> GetTurns();
		Turn GetCurrent();
		void AddLast(Turn turn);
		void Delete(TurnId id);
	}
}