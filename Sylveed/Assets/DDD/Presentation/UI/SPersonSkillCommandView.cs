using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sylveed.DDD.Presentation.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;


namespace Sylveed.DDD.Presentation.UI
{
	public class SPersonSkillCommandView : UIBehaviour
	{
		[SerializeField]
		Button skill1Button;
		[SerializeField]
		Button skill2Button;
		[SerializeField]
		Button skill3Button;

		SPerson Player => ServiceManager.PersonService.Player;

		readonly Dictionary<int, Button> skillButtonMap = new Dictionary<int, Button>();

		protected override void Awake()
		{
			skillButtonMap.Add(1, skill1Button);
			skillButtonMap.Add(2, skill1Button);
			skillButtonMap.Add(3, skill1Button);

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
