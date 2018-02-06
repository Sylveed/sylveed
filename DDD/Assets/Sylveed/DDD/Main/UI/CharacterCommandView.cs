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
using UniRx;
using Assets.Sylveed.DDD.Main.Domain.Skills;

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
            ServiceResolver.Resolve(this);
			ComponentResolver.Resolve(this);

			skillButtonMap.Add(0, skill1Button);
			skillButtonMap.Add(1, skill2Button);
			skillButtonMap.Add(2, skill3Button);

			skillButtonMap.ForEach(x =>
			{
				x.Value.onClick.AddListener(() => UseSkill(x.Key));
			});

			Observable.EveryUpdate()
				.Select(_ =>
				{
					if (Input.GetKeyDown(KeyCode.Z))
						return 0;
					if (Input.GetKeyDown(KeyCode.X))
						return 1;
					if (Input.GetKeyDown(KeyCode.C))
						return 2;
					return -1;
				})
				.Where(x => x >= 0)
				.Subscribe(skillIndex =>
				{
					UseSkill(skillIndex);
				})
				.AddTo(this);
		}

		void UseSkill(int index)
		{
			var playerDir = Quaternion.AngleAxis(Player.Angle, Vector3.up) * Vector3.forward;

			var targetCharacter = characterService.Items
				.Where(x => x != Player)
				.OrderBy(x => Vector3.Angle((x.Position - Player.Position).normalized, playerDir))
				.FirstOrDefault();

			if (targetCharacter != null)
			{
				Player.UseSkill(index, SkillTarget.Create(targetCharacter.Position));
			}
			else
			{
				Player.UseSkill(index);
			}
		}
	}
}
