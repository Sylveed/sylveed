using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sylveed.MapEditor.Components
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(SpriteRenderer))]
	public class MapChip : MonoBehaviour
	{
		[SerializeField]
		MapGrid grid;
		[SerializeField]
		string mapSourceId;
		[SerializeField]
		int x;
		[SerializeField]
		int y;

		SpriteRenderer spriteRenderer;

		public string MapSourceId { get { return mapSourceId; } }
		public int X { get { return x; } }
		public int Y { get { return y; } }

		public void SetMapSourceId(string mapSourceId) { this.mapSourceId = mapSourceId; }
		public void SetX(int value) { this.x = value; }
		public void SetY(int value) { this.y = value; }

		void Start()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			
			grid = grid ?? (GetComponentInParent<MapGrid>());

			if (grid != null)
			{
				grid.Warmup();

				spriteRenderer.sprite = grid.GetSpriteOrDefault(mapSourceId, x, y);
			}
		}

		private void OnValidate()
		{
			if (grid != null)
			{
				spriteRenderer.sprite = grid.GetSpriteOrDefault(mapSourceId, x, y);
			}
		}
	}
}
