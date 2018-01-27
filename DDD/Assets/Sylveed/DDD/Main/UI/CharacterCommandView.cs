using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using Assets.Sylveed.ComponentDI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Assets.Sylveed.DDD.Main.UI
{
	public class CharacterCommandView : UIBehaviour
	{
		[DINamedComponent]
		RectTransform skillButtonContainer;

		[DINamedComponent(nameof(skillButtonContainer))]
		Button skill1Button;
		[DINamedComponent(nameof(skillButtonContainer))]
		Button skill2Button;
		[DINamedComponent(nameof(skillButtonContainer))]
		Button skill3Button;
		[DITypedComponent(nameof(skillButtonContainer))]
		Button[] skillButtons;

		CharacterVmService personService;

		CharacterVm Player => personService.Player;

		readonly Dictionary<int, Button> skillButtonMap = new Dictionary<int, Button>();

		protected override void Awake()
		{
            ServiceResolver.Resolve(out personService);
			ComponentResolver.Resolve(this);

			skillButtonMap.Add(0, skill1Button);
			skillButtonMap.Add(1, skill2Button);
			skillButtonMap.Add(2, skill3Button);

			skillButtonMap.ForEach(x =>
			{
				x.Value.onClick.AddListener(() => UseSkill(new CharacterVmSkillIndex(x.Key)));
			});
			foreach (var x in skillButtons)
				Debug.Log(x);
		}

		void UseSkill(CharacterVmSkillIndex index)
		{
			Player.UseSkill(index);
		}
	}
}
