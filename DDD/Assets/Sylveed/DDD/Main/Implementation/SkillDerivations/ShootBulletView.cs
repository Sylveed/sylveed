using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.ComponentDI;
using UniRx;
using UniRx.Triggers;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using Assets.Sylveed.DDD.Main.Implementation.Helpers;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Main.Implementation.SkillDerivations
{
	public class ShootBulletView : MonoBehaviour, ISkillView, IInjectComponent
	{
		[DITypedComponent]
		readonly new Rigidbody rigidbody;
		[DITypedComponent]
		readonly Collider collider;

		public Vector3 Position
		{
			get { return transform.position; }
		}

		public float Angle
		{
			get { return transform.localEulerAngles.y; }
		}

		[Inject]
		void Initialize()
		{
			this.OnCollisionEnterAsObservable()
				.First()
				.Subscribe(collision =>
				{
					Destroy(rigidbody);
					Destroy(collider);

					this.UpdateAsObservable()
						.Do(_ => transform.localScale += Vector3.one * 10 * Time.deltaTime)
						.TakeUntil(Observable.ReturnUnit().Delay(TimeSpan.FromSeconds(0.2f)))
						.LastOrDefault()
						.Subscribe(_ => Destroy(gameObject));
				});
		}

		public void Shoot(Vector3 direction, float speed)
		{
			rigidbody.AddForce(direction.normalized * speed, ForceMode.Impulse);
		}

		public IObservable<Collision> CollidedWithCharacter
		{
			get
			{
				return this.OnCollisionEnterAsObservable()
					.Where(x => x.transform.GetComponent<ICharacterView>() != null);
			}
		}
	}
}
