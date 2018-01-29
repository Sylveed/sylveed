using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Data.Characters;

namespace Assets.Sylveed.DDD.Main.Domain.Players
{
	public class CpuPlayer : IPlayer
	{
		public PlayerId Id { get; }

		public string Name { get; }

		public CharacterId CharacterId { get; }
		
		public CpuPlayer(PlayerId id, string name, CharacterId characterId)
		{
			Id = id;
			Name = name;
			CharacterId = characterId;
		}
	}
}
