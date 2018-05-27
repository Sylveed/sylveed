using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Sylveed.Ido.Domain.Battles
{
	public interface IBattlerOperatorRepository
	{
		IBattlerOperator Get(BattlerId id);
		void Add(BattlerId id, IBattlerOperator @operator);
	}
}