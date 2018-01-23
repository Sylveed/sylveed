using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sylveed.DDD.Presentation.Model;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;

namespace Sylveed.DDD.Presentation.UI
{
	public class TouchView : UIBehaviour
	{
		SPerson Player => ServiceManager.PersonService.Player;

		protected override void Awake()
		{
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

					Player.MoveTo(dir, speed);
				})
				.AddTo(subscription);

			return subscription;
		}
	}
}
