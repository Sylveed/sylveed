using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Domain.Skills;

namespace Assets.Sylveed.DDD.Main.Domain.SPersons
{
	public class SPerson
	{
		readonly IView view;
		readonly SPersonSkillSet skillSet;

		public SPersonId Id { get; }

		public Vector3 Position { get { return view.Position; } }

		public float Angle { get { return view.Angle; } }

		public SPerson(Parameter parameter)
		{
			Id = parameter.Id;

			view = parameter.View;
			skillSet = parameter.SkillSet;
		}

		public void UseSkill(SPersonSkillIndex index)
		{
			
		}

		public void MoveTo(Vector3 direction, float speed)
		{
			
		}

		public interface IView
		{
			float Speed { get; set; }
			void MoveTo(Vector3 direction);
			void ShowSkill(Skill skill);
			Vector3 Position { get; set; }
			float Angle { get; set; }
		}

		public class Parameter
		{
			public SPersonId Id { get; }
			public string Name { get; }
			public IView View { get; }
			public SPersonSkillSet SkillSet { get; }

			public Parameter(SPersonId id, string name, IView view, SPersonSkillSet skillSet)
			{
				Id = id;
				View = view;
				SkillSet = skillSet;
			}
		}
	}
}
