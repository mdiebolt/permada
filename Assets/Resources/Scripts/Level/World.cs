using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Level {
	public class World : MonoBehaviour {
    private int width = 1;
    private int height = 1;
    private Vector2 currentPosition = Vector2.zero;
    private List<GameObject> areas = new List<GameObject>();

    private List<string> levels = new List<string> { 
      "forest", 
      "forest1", 
      "maze"
    };
    
		void Start() {
      generate();
		}

    public void UpdateCurrentPosition(int x, int y) {
      var amount = new Vector2(x, y);
      currentPosition += amount;

      foreach (var area in areas) {
        area.SetActive(area.tag == currentCoordinates());
      }
    }

    private string currentCoordinates() {
      return string.Format("({0}, {1})", currentPosition.x, currentPosition.y);
    }

    private GameObject createArea(string name, int x, int y) {
      GameObject area = new GameObject();
      var coordinates = string.Format("({0}, {1})", x, y);
      area.name = string.Format("{0} {1}", name, coordinates);
      // tag the area with a tuple representing its position
      // so we can easily look it up later
      area.tag = coordinates;
      area.transform.parent = gameObject.transform;

      return area;
    }

    private void load(string name, int xOffset, int yOffset) {
      var mapData = Tilemap.Load(name);

      GameObject area = createArea(name, xOffset, yOffset);
      areas.Add(area);

      foreach (var tile in mapData) {
        var t = tile.Instantiate(xOffset, yOffset);
        t.transform.parent = area.transform;
      }
    }

    private void generate() {
      for (int i = -width; i <= width; i++) {
        for (int j = -height; j <= height; j++) {
          var index = Random.Range(0, levels.Count);
          var level = levels[index];

          load(level, i, j);
        }
      }

      // Call to deactivate non-active areas
      UpdateCurrentPosition(0, 0);
    }
	}
}