using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Data.Characters;

namespace Assets.Sylveed.DDD.Main.Domain.Players
{
	public interface IPlayer
	{
		PlayerId Id { get; }
		string Name { get; }
		CharacterId CharacterId { get; }
	}
}
