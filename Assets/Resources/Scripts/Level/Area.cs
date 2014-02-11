using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Level {
	public class Area : MonoBehaviour {
		private void addToParent(GameObject obj) {
			obj.transform.parent = GameObject.Find("ActiveArea").transform;
		}

		public void Load(string name, int xOffset, int yOffset) {
      var mapData = Tilemap.Load(name);

      foreach (var tile in mapData) {
        var position = tile.Position + new Vector3(xOffset, yOffset, 0);
        var obj = Instantiate(tile.TilePrefab, position, Quaternion.identity) as GameObject;
        addToParent(obj);
      }
		}
	}
}	