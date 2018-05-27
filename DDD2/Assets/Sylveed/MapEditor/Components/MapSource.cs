using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Sylveed.MapEditor.Components
{
	public class MapSource : ScriptableObject
	{
		[SerializeField]
		string id;
		[SerializeField]
		string spriteDirectory;
		[SerializeField]
		string spriteName;
		[SerializeField]
		int cellXCount;
		[SerializeField]
		int cellYCount;

		public string Id { get { return id; } }
		
		public Sprite[][] LoadSpriteMatrix()
		{
			var sprites = Resources.LoadAll<Sprite>(spriteDirectory + "/" + spriteName);
			
			return Enumerable.Range(0, cellYCount)
				.Select(y =>
				{
					return Enumerable.Range(0, cellXCount)
						.Select(x =>
						{
							return sprites[y * cellXCount + x];
						})
						.ToArray();
				})
				.ToArray();
		}
	}
}
