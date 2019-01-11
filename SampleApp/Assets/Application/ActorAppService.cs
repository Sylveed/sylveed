using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sylveed.SampleApp
{
	public class ActorAppService
	{
		[Resolve]
		IActorRepository actorRepository;
		[Resolve]
		IActorFactory actorFactory;

		readonly Dictionary<ActorId, ActorDto> actorDtos = new Dictionary<ActorId, ActorDto>();

		[Resolve]
		void Initialize()
		{
			foreach(var entity in actorRepository.Actors)
			{
				actorDtos.Add(entity.Id, new ActorDto(entity));
			}

			DomainEventPublisher.Subscribe<IRepositoryAdded<Actor>>(e =>
			{
				actorDtos.Add(e.Entity.Id, new ActorDto(e.Entity));
			});

			DomainEventPublisher.Subscribe<IRepositoryRemoved<Actor>>(e =>
			{
				actorDtos.Remove(e.Entity.Id);
			});
		}

		public ActorDto AddActor()
		{
			var actor = actorFactory.Create();

			actorRepository.Add(actor);

			return actorDtos[actor.Id];
		}

		public void DeleteActor(ActorId id)
		{
			actorRepository.Remove(id);
		}
	}

}