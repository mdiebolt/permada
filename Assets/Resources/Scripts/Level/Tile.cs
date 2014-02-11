using UnityEngine;
using System.Collections;

namespace Level {
  public class Tile {
    private const int TILES_WIDE = 25;
    private const int TILES_HIGH = 25;
    
    private Vector3 position;
    private GameObject prefab;
    
    public Tile(Vector3 pos, GameObject tilePrefab) {
      position = pos;
      prefab = tilePrefab;
    }
    
    public GameObject Instantiate(int xOffset, int yOffset) {
      var offset = new Vector3(xOffset * TILES_WIDE, yOffset * TILES_HIGH, 0);
      var pos = position + offset;

      return GameObject.Instantiate(prefab, pos, Quaternion.identity) as GameObject;
    }
  }
}