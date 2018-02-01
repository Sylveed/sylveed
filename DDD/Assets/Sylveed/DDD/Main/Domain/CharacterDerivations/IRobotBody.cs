using Assets.Sylveed.DDD.Main.Domain.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Sylveed.DDD.Main.Domain.CharacterDerivations
{
	public interface IRobotBody : ICharacterBody
	{
		Vector3 NozzleDirection { get; }
		Vector3 NozzleStubPosition { get; }
	}
}
