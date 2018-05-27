using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Sylveed.Ido.Domain.Adventures;

namespace Sylveed.Ido.UseCases.Adventures
{
	public class AdventureUseCase
	{
		public AdventureModel GetCurrentAdventure()
		{
			throw new NotImplementedException();
		}

		public EventModel GetCurrentEvent()
		{
			throw new NotImplementedException();
		}

		public void ProcessToNextEvent()
		{
			var currentEvent = GetCurrentEvent();
			if (!currentEvent.IsCompleted)
				throw new InvalidOperationException();

			throw new NotImplementedException();
		}
	}
}