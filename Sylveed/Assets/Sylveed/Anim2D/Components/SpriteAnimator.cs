using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UniRx;

namespace Sylveed.Anim2D
{
	public class SpriteAnimator : MonoBehaviour
	{
		[SerializeField]
		float speed = 1f;
		[SerializeField]
		SpriteRenderer spriteRenderer;

		IDisposable currentPlayingCancellation;

		public SpriteAnimation CurrentAnimation { get; private set; }

		public bool IsPlaying { get { return currentPlayingCancellation != null; } }

		public IDisposable Play(SpriteAnimation animation)
		{
			Clear();

			CurrentAnimation = animation;
			
			return currentPlayingCancellation = animation.GetSprites()
				.DoOnTerminate(() => Clear())
				.Subscribe(sprite =>
				{
					spriteRenderer.sprite = sprite;
				}, Debug.LogException);
		}

		public IDisposable PlayLoop(SpriteAnimation animation)
		{
			Clear();

			CurrentAnimation = animation;

			return currentPlayingCancellation = animation.GetSprites()
				.Repeat()
				.DoOnTerminate(() => Clear())
				.Subscribe(sprite =>
				{
					spriteRenderer.sprite = sprite;
				}, Debug.LogException);
		}

		void Clear()
		{
			if (currentPlayingCancellation != null)
			{
				currentPlayingCancellation.Dispose();
				currentPlayingCancellation = null;
			}

			CurrentAnimation = null;
		}
	}
}
