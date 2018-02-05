using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Data.Skills;

namespace Assets.Sylveed.DDD.Main.Domain.Characters
{
    public interface ICharacterBody
	{
		bool CanControl { get; }
    }
}
