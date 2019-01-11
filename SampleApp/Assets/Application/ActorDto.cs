using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sylveed.SampleApp
{
	public class ActorDto
	{
		readonly Actor actor;

		public ActorId Id { get { return actor.Id; } }
		
		public ActorDto(Actor actor)
		{
			this.actor = actor;
		}
	}

}