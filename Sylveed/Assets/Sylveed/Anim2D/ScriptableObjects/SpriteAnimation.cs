using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UniRx;

namespace Assets.Sylveed.Anim2D
{
	public class SpriteAnimation : ScriptableObject
	{
		[SerializeField]
		Frame[] frames;
		[SerializeField]
		float totalTime = 3f;

		public float TotalTime { get { return totalTime; } }
		
		public IObservable<Sprite> GetSprites()
		{/*
			return Observable.Create<Sprite>(observer =>
			{
				if (frames.Length == 0)
				{
					observer.OnCompleted();
					return Disposable.Empty;
				}

				var totalSpeed = frames.Select(x => x.Speed).Sum();

				observer.OnNext(frames[0].Sprite);

				var d1 = frames.Skip(1).Select((frame, i) =>
				{
					var prevFrame = frames[i];

					var duration = (prevFrame.Speed / totalSpeed) * totalTime;

					return Observable.Return(frame.Sprite)
						.Delay(TimeSpan.FromSeconds(duration));
				})
				.Concat()
				.Subscribe(observer.OnNext, observer.OnError);

				var d2 = Observable.ReturnUnit()
					.Delay(TimeSpan.FromSeconds(totalTime))
					.Subscribe(observer.OnCompleted);

				return StableCompositeDisposable.Create(d1, d2);
			});*/
			return Observable.FromCoroutine<Sprite>(observer => GetSpriteRoutine(observer));
		}

		IEnumerator GetSpriteRoutine(IObserver<Sprite> observer)
		{
			if (frames.Length == 0)
			{
				observer.OnCompleted();
				yield break;
			}

			var totalSpeed = frames.Select(x => x.Speed).Sum();

			var timer = 0f;
			var currentIndex = 0;
			var currentFrame = frames[currentIndex];
			var currentDuration = (currentFrame.Speed / totalSpeed) * totalTime;

			observer.OnNext(currentFrame.Sprite);

			while (true)
			{
				if (timer >= currentDuration)
				{
					timer -= currentDuration;
					if (++currentIndex >= frames.Length)
						break;

					currentFrame = frames[currentIndex];
					currentDuration = (currentFrame.Speed / totalSpeed) * totalTime;

					observer.OnNext(currentFrame.Sprite);
				}

				yield return null;

				timer += Time.deltaTime;
			}

			observer.OnCompleted();
		}
		
		[Serializable]
		class Frame
		{
			[SerializeField]
			Sprite sprite;
			[SerializeField]
			float speed = 1f;

			public Sprite Sprite { get { return sprite; } }
			public float Speed { get { return speed; } }
		}
	}
}
