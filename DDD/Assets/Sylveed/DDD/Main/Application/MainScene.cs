using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using Assets.Sylveed.DDD.Main.Domain.Players;
using Assets.Sylveed.DDD.Data.Characters;

namespace Assets.Sylveed.DDD.Main
{
	public class MainScene : MonoBehaviour
	{
		CharacterVmService characterService;
		PlayerService playerService;

		void Awake()
		{
			ServiceResolver.Resolve(out characterService);
			ServiceResolver.Resolve(out playerService);

			var player = characterService.Create(playerService.GetLocalUserPlayer());

			var npc = characterService.Create(playerService.CpuPlayers.First());
		}
	}
}
