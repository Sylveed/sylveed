using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Domain.Characters;

namespace Assets.Sylveed.DDD.Main
{
	public class MainScene : MonoBehaviour
	{
		CharacterVmService personService;

		void Awake()
		{
			ServiceResolver.Resolve(out personService);

			var player = personService.Create("first player");

			personService.SetPlayer(player.Id);
		}
	}
}
