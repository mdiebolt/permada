using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Level {
	public class World : MonoBehaviour {
    private int width = 1;
    private int height = 1;

		void Start() {
      generate();
		}

    private void addToParent(GameObject obj) {
      obj.transform.parent = gameObject.transform;
    }

    private void load(string name, int xOffset, int yOffset) {
      var mapData = Tilemap.Load(name);
      
      foreach (var tile in mapData) {
        addToParent(tile.Instantiate(xOffset, yOffset));
      }
    }

    private void generate() {
      for (int i = -width; i <= width; i++) {
        for (int j = -height; j <= height; j++) {
          // TODO: pick areas intelligently
          var num = Random.Range(0, 10);

          if(num <= 5) {
            load("forest", i, j);
          } else {
            load("maze", i, j);
          }
        }
      }
    }
	}
}