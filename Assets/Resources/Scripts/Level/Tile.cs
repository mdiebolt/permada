using UnityEngine;
using System.Collections;

namespace Level {
  public class Tile {
    private const int TILES_WIDE = 25;
    private const int TILES_HIGH = 25;
    
    private Vector3 position;
    private GameObject prefab;
    private string layer;
    
    public Tile(Vector3 pos, string layerName, GameObject tilePrefab) {
      position = pos;
      prefab = tilePrefab;
      layer = layerName;
    }
    
    public GameObject Instantiate(int xOffset, int yOffset) {
      var offset = new Vector3(xOffset * TILES_WIDE, yOffset * TILES_HIGH, 0);
      var pos = position + offset;

      var obj = GameObject.Instantiate(prefab, pos, Quaternion.identity) as GameObject;
      obj.renderer.sortingLayerName = layer;

      return GameObject.Instantiate(prefab, pos, Quaternion.identity) as GameObject;
    }
  }
}