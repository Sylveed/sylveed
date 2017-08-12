using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Sylveed.Anim2D;
using UniRx;

namespace Sylveed.Avicia.Presentation.Main
{
	public class TestAnimator : MonoBehaviour
	{
		[SerializeField]
		SpriteAnimation idleAnimation;
		[SerializeField]
		SpriteAnimation walkAnimation;

		[SerializeField]
		bool walk = false;
		[SerializeField]
		bool attack = false;

		SpriteAnimator animator;

		void Awake()
		{
			animator = GetComponent<SpriteAnimator>();
		}

		void Update()
		{
			if (animator.CurrentAnimation != walkAnimation && walk)
			{
				animator.PlayLoop(walkAnimation).AddTo(this);
			}
			else if (animator.CurrentAnimation != idleAnimation && !walk)
			{
				animator.PlayLoop(idleAnimation).AddTo(this);
			}
		}
	}
}
