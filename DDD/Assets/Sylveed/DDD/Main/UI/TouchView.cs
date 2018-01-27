using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Sylveed.ComponentDI;

namespace Assets.Sylveed.DDD.Main.UI
{
	public class TouchView : UIBehaviour
	{
		[DITypedComponent]
		JoyStickView joyStickView;

        CharacterVmService personService;

		CharacterVm Player => personService.Player;

		protected override void Awake()
		{
			ServiceResolver.Resolve(out personService);
			ComponentResolver.Resolve(this);

			joyStickView.gameObject.SetActive(false);

			Moving().AddTo(this);
		}

		IDisposable Moving()
		{
			var subscription = new CompositeDisposable();

			var initialPoint = default(Vector2?);
			var currentDelta = Vector2.zero;

			this.OnBeginDragAsObservable()
				.Subscribe(beginEvent =>
				{
					initialPoint = beginEvent.position;

					joyStickView.gameObject.SetActive(true);
					joyStickView.transform.localPosition = transform.InverseTransformPoint(
						beginEvent.pressEventCamera.ScreenToWorldPoint(beginEvent.position));

					this.UpdateAsObservable()
						.TakeUntil(this.OnEndDragAsObservable())
						.Subscribe(_ =>
						{
							var dir = currentDelta.normalized;
							var speedRatio = joyStickView.SetMovement(currentDelta);

							Player.SetDestinationDirection(new Vector3(dir.x, 0, dir.y), speedRatio);
						});
				})
				.AddTo(subscription);

			this.OnDragAsObservable()
				.Subscribe(e =>
				{
					currentDelta = e.position - initialPoint.Value;
				});

			this.OnEndDragAsObservable()
				.Subscribe(_ =>
				{
					joyStickView.gameObject.SetActive(false);
					initialPoint = null;
					currentDelta = Vector2.zero;

					Player.StopMovement();
				});

			return subscription;
		}
	}
}
