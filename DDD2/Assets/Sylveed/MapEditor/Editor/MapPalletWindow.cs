using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

namespace Sylveed.MapEditor.Components.Editor
{
	public class MapPalletWindow : EditorWindow
	{
		[MenuItem("Window/Sylveed/MapEditor/Map Pallet")]
		static MapPalletWindow Open()
		{
			return GetWindow<MapPalletWindow>();
		}

		void OnGUI()
		{
			var selectedGo = Selection.activeGameObject;
			if (selectedGo == null)
				return;

			var grid = selectedGo.GetComponent<MapGrid>();
			if (grid == null)
				return;

			Matrix4x4 oldMatrix = GUI.matrix;

			//Scale my gui matrix
			Vector2 vanishingPoint = new Vector2(0, 20);
			Matrix4x4 Translation = Matrix4x4.TRS(vanishingPoint, Quaternion.identity, Vector3.one);
			Matrix4x4 Scale = Matrix4x4.Scale(new Vector3(zoomScale, zoomScale, 1.0f));
			GUI.matrix = Translation * Scale * Translation.inverse;

			var spriteMatrix = grid.MapSources
				.Select(x => x.LoadSpriteMatrix())
				.FirstOrDefault();

			var sourceTexture = spriteMatrix[0][0].texture;
			var cellSize = spriteMatrix[0][0].rect.size;

			var textureRect = new Rect(
				new Vector2(0, 0), 
				new Vector2(sourceTexture.width, sourceTexture.height));

			EditorGUI.DrawPreviewTexture(textureRect, sourceTexture);
			/*
		foreach (var textures in spriteMatrix)
		{
			foreach (var tex in textures)
			{
			}
		}
		*/
			/*var selectionRect = new Rect(
				Vector2.Scale(new Vector2(selectedCellX, selectedCellY), cellSize), cellSize);*/
			var selectionRect = new Rect(
				new Vector2(selectedCellX * cellSize.x, selectedCellY * cellSize.y), cellSize);

			EditorGUI.DrawRect(selectionRect, new Color(1, 0, 0, 0.4f));


			// handle click
			if (Event.current.type == EventType.MouseUp)
			{
				var e = Event.current;
				var mousePos = e.mousePosition;
				selectedCellX = Mathf.FloorToInt(mousePos.x / cellSize.x);
				selectedCellY = Mathf.FloorToInt(mousePos.y / cellSize.y);
				e.Use();
				Repaint();
			}

			//reset the matrix
			GUI.matrix = oldMatrix;

			// handle zoom
			if (Event.current.type == EventType.ScrollWheel)
			{
				var e = Event.current;
				var zoomDelta = 0.1f;
				zoomDelta = e.delta.y < 0 ? zoomDelta : -zoomDelta;
				zoomScale += zoomDelta;
				zoomScale = Mathf.Clamp(zoomScale, 0.25f, 3.25f);
				e.Use();
			}
		}


		float zoomScale = 1.0f;
		int selectedCellX = 0;
		int selectedCellY = 0;
	}
}
