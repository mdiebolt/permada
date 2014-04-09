using UnityEngine;
using System.Collections;

public class Tile {
  private Vector3 position;
  private GameObject prefab;
  private string layer;
  
  public Tile(Vector3 pos, string layerName, GameObject tilePrefab) {
    position = pos;
    prefab = tilePrefab;
    layer = layerName;
  }
  
  public GameObject Instantiate(int xOffset, int yOffset) {
    var offset = new Vector3(xOffset * World.AREA_WIDTH, yOffset * World.AREA_HEIGHT, 0);
    var pos = position + offset;

    var obj = GameObject.Instantiate(prefab, pos, Quaternion.identity) as GameObject;
    obj.renderer.sortingLayerName = layer;

    return obj;
  }
}
