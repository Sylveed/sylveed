using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Sylveed.SampleApp
{
	public static class DomainEventPublisher
	{
		public static void Publish<T>(T domainEvent)
		{
			throw new NotImplementedException();
		}

		public static IDisposable Subscribe<T>(Action<T> handler)
		{
			throw new NotImplementedException();
		}
	}

}