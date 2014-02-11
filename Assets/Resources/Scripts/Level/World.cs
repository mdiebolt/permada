using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Level {
	public class World : MonoBehaviour {
    public List<Area> world;
    public int width = 1;
    public int height = 1;

		private Area activeArea;

		void Start() {
      generate();
      // TODO activate one of the areas
		}

    private void generate() {
      for (int i = 0; i < width; i++) {
        for (int j = 0; i < height; j++) {
          // TODO: pick areas intelligently
          var area = gameObject.AddComponent<Area>();
          area.Load("forest", i, j);
          world.Add(area);
        }
      }
    }
	}
}