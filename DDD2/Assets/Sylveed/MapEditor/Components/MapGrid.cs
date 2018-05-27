using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Sylveed.MapEditor.Components
{
	[ExecuteInEditMode]
	public class MapGrid : MonoBehaviour
	{
		[SerializeField]
		MapSource[] mapSources;

		public IEnumerable<MapSource> MapSources { get { return mapSources; } }

		Dictionary<string, Sprite[][]> mapSourceSpriteMap;

		bool isInitialized = false;

		public void Warmup()
		{
			if (isInitialized) return;

			isInitialized = true;

			mapSourceSpriteMap = mapSources.ToDictionary(x => x.Id, x =>
			{
				return x.LoadSpriteMatrix();
			});
		}

		public Sprite GetSpriteOrDefault(string mapSourceId, int x, int y)
		{
			if (x < 0 || y < 0) return null;

			Sprite[][] matrix;
			if (mapSourceSpriteMap.TryGetValue(mapSourceId, out matrix))
			{
				if (matrix.Length > y && matrix[y].Length > x)
				{
					return matrix[y][x];
				}
			}

			return null;
		}

		void Start()
		{
			Warmup();
		}
	}
}
