using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Sylveed.DDD.Main.UI
{
	public class JoyStickView : UIBehaviour
	{
		[SerializeField]
		RectTransform stick;
		[SerializeField]
		RectTransform movableArea;

		Rect movableRect;

		protected override void Awake()
		{
			movableRect = movableArea.rect;
		}

		public float SetMovement(Vector2 movement)
		{
			var halfSize = movableRect.size / 2;
			var ratio = Mathf.Clamp01(movement.magnitude / halfSize.x);
			stick.localPosition = Vector3.Scale(movement.normalized, halfSize) * ratio;
			return ratio;
		}
	}
}
