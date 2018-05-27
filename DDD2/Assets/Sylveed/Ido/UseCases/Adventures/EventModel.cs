using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Sylveed.Ido.UseCases.Adventures
{
	public class EventModel
	{
		public EventType EventType { get; private set; }
		public bool IsCompleted { get; private set; }
	}
}