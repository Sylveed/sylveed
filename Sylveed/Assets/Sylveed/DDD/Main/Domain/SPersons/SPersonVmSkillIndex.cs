﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Sylveed.DDD.Main.Domain.SPersons
{
	public struct SPersonVmSkillIndex
	{
		public const int Max = 3;

		int value;

		public int Value
		{
			get { return value; }
			set { SetValue(value); }
		}

		public SPersonVmSkillIndex(int value) : this()
		{
			SetValue(value);
		}

		void SetValue(int value)
		{
			if (value >= Max)
				throw new ArgumentException();

			this.value = value;
		}
	}
}