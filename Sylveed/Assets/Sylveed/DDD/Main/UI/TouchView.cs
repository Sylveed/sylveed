using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Main.Domain.SPersons;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Sylveed.DDD.Main.UI
{
	public class TouchView : UIBehaviour
	{
        SPersonVmService personService;

		SPersonVm Player => personService.Player;

		protected override void Awake()
		{
			ServiceResolver.Resolve(out personService);

			Moving().AddTo(this);
		}

		IDisposable Moving()
		{
			var subscription = new CompositeDisposable();

			var initialPoint = default(Vector2?);

			this.OnBeginDragAsObservable()
				.Subscribe(e =>
				{
					initialPoint = e.position;
				})
				.AddTo(subscription);

			this.OnDragAsObservable()
				.Subscribe(e =>
				{
					var delta = e.position - initialPoint.Value;

					var dir = delta.normalized;
					var speed = delta.magnitude;

					Player.MoveTo(new Vector3(dir.x, 0, dir.y), speed);
				})
				.AddTo(subscription);

			return subscription;
		}
	}
}
