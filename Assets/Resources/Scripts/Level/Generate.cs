using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Camera;

namespace Level {
	public class Generate : MonoBehaviour {
		public Area activeArea;

    private int MapX = 0;
    private int MapY = 0;

		void Start() {
      CreateArea();
		}

    private void CreateArea() {
      activeArea = gameObject.AddComponent<Area>();
      activeArea.LoadAt(MapX, MapY);
    }

    public void Load(int x, int y) {
      MapX = x;
      MapY = y;

      CreateArea();
    }
	}
}