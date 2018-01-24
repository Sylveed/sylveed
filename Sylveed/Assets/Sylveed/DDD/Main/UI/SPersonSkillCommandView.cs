using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Main.Domain.SPersons;
using Assets.Sylveed.DDDTools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;


namespace Assets.Sylveed.DDD.Main.UI
{
	public class SPersonSkillCommandView : UIBehaviour
	{
		[SerializeField]
		Button skill1Button;
		[SerializeField]
		Button skill2Button;
		[SerializeField]
		Button skill3Button;

        [Inject]
        readonly SPersonService personService;

		SPerson Player => personService.Player;

		readonly Dictionary<int, Button> skillButtonMap = new Dictionary<int, Button>();

		protected override void Awake()
		{
            ServiceResolver.ResolveMembers(this);

			skillButtonMap.Add(0, skill1Button);
			skillButtonMap.Add(1, skill2Button);
			skillButtonMap.Add(2, skill3Button);

			skillButtonMap.ForEach(x =>
			{
				x.Value.onClick.AddListener(() => UseSkill(new SPersonSkillIndex(x.Key)));
			});
		}

		void UseSkill(SPersonSkillIndex index)
		{
			Player.UseSkill(index);
		}
	}
}
