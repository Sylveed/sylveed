using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sylveed.SampleApp
{
	public interface IActorRepository
	{
		IEnumerable<Actor> Actors { get; }
		Actor GetActor(ActorId id);
		void Add(Actor actor);
		void Remove(ActorId id);
	}

}