using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Level {
	public class Area : MonoBehaviour {
		public List<GameObject> tiles;

		private GameObject boringGrass;
		private GameObject grass;
		private GameObject rock;
		private GameObject bush;

		public string cardinal { get; set; } 

		/// <summary>
		/// Initializes a new instance of the <see cref="Level.Area"/> class.
		/// Represents one screen of the map, loading an adjacent map when
		/// the player moves past the screen edge.
		/// </summary>
		public Area() {
			tiles = new List<GameObject>();

			boringGrass = Resources.Load<GameObject>("Prefabs/EmptyGrass");
			grass = Resources.Load<GameObject>("Prefabs/Grass");
			rock = Resources.Load<GameObject>("Prefabs/Rock");
			bush = Resources.Load<GameObject>("Prefabs/Bush");
		}

		/// <summary>
		/// Picks a random map tile prefab for use with 
		/// procedural level generation.
		/// </summary>
		/// <returns>Prefab of the random tile selected.</returns>
		private GameObject PickRandomTile() {
			var number = Random.Range(0, 10);
			var prefab = grass;
			
			if (number < 5) {
				if (number >= 2)
					prefab = boringGrass;
			} else {
				if (number > 5)
					prefab = bush;
				else
					prefab = rock;
			}
			
			return prefab;
		}

		private void AddToParent(GameObject obj) {
			obj.transform.parent = GameObject.Find(cardinal).transform;
		}

		/// <summary>
		/// Cleans up all tiles in an area.
		/// Call this when an Area is unloaded.
		/// </summary>
		public void CleanUpTiles() {
			foreach (var tile in tiles) {
				Destroy(tile);
			}
		}

		/// <summary>
		/// Loads an area with a given offset. The
		/// offsets are defined in terms of complete 
		/// areas so (1, 0) load a map to the right
		/// of the one defined at (0, 0) at the same
		/// y position.
		/// </summary>
		/// <param name="xOffset">X offset in area coordinates</param>
		/// <param name="yOffset">Y offset in area coordinates</param>
		public void LoadAt(int xOffset, int yOffset) {
			float halfWidth = 0.5f;
			float halfHeight = 0.5f;

			float xMin = (xOffset * 12) + halfWidth;
			float xMax = xMin + 24;
			
			float yMin = (yOffset * 12) + halfHeight;
			float yMax = yMin + 24;
			
			for (float x = xMin; x < xMax; x++) {
				for (float y = yMin; y < yMax; y++) {
					var prefab = PickRandomTile();
					var obj = Instantiate(prefab, new Vector3(x, y), Quaternion.identity) as GameObject;

					if (prefab.name == "Bush") {
						obj.renderer.sortingOrder = 1;

						var grass = Instantiate(boringGrass, new Vector3(x, y), Quaternion.identity) as GameObject;
						grass.renderer.sortingOrder = 0;
						AddToParent(grass);
						tiles.Add(grass);
					}

					AddToParent(obj);
					tiles.Add(obj);
				}
			}
		}
	}
}	