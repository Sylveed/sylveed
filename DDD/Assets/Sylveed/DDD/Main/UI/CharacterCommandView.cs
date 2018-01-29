using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using Assets.Sylveed.DDD.Main.Domain.Players;
using Assets.Sylveed.ComponentDI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.Sylveed.DDDTools;


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

		[Inject]
		readonly CharacterVmService characterService;

		CharacterVm Player => characterService.GetLocalUser();

		readonly Dictionary<int, Button> skillButtonMap = new Dictionary<int, Button>();

		protected override void Awake()
		{
            ServiceResolver.ResolveMembers(this);
			ComponentResolver.Resolve(this);

			skillButtonMap.Add(0, skill1Button);
			skillButtonMap.Add(1, skill2Button);
			skillButtonMap.Add(2, skill3Button);

			skillButtonMap.ForEach(x =>
			{
				x.Value.onClick.AddListener(() => UseSkill(x.Key));
			});
			foreach (var x in skillButtons)
				Debug.Log(x);
		}

		void UseSkill(int index)
		{
			Player.UseSkill(index);
		}
	}
}
