using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sylveed.SampleApp
{
	public class Force
	{
		public PowerUnit Power { get; private set; }
		public Element Element { get; private set; }

		public Force(PowerUnit power, Element element)
		{
			Power = power;
			Element = element;
		}
	}

}