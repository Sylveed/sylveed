using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Assets.Sylveed.DDD.Main.UI
{
	public class CharacterSkillCommandView : UIBehaviour
	{
		[SerializeField]
		Button skill1Button;
		[SerializeField]
		Button skill2Button;
		[SerializeField]
		Button skill3Button;
		
        CharacterVmService personService;

		CharacterVm Player => personService.Player;

		readonly Dictionary<int, Button> skillButtonMap = new Dictionary<int, Button>();

		protected override void Awake()
		{
            ServiceResolver.Resolve(out personService);

			skillButtonMap.Add(0, skill1Button);
			skillButtonMap.Add(1, skill2Button);
			skillButtonMap.Add(2, skill3Button);

			skillButtonMap.ForEach(x =>
			{
				x.Value.onClick.AddListener(() => UseSkill(new CharacterVmSkillIndex(x.Key)));
			});
		}

		void UseSkill(CharacterVmSkillIndex index)
		{
			Player.UseSkill(index);
		}
	}
}
