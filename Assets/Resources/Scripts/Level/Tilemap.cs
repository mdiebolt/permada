using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Procurios.Public;
using System.IO;
using System;

public class Tilemap : MonoBehaviour {
  private Hashtable data;

  private int tilesWide;
  private int tilesHigh;

  private Dictionary<int, string> tilesetMap;

  private void PopulateTilesetMap() {
    foreach (Hashtable set in (ArrayList)data["tilesets"]) {
      Hashtable properties = (Hashtable)set["tileproperties"];
      foreach (DictionaryEntry pair in properties) {
        Hashtable value = (Hashtable)pair.Value;
        
        tilesetMap.Add(int.Parse(pair.Key.ToString()), value["Prefab"].ToString());
      }
    }
  }

  private void SetTileData() {
    tilesWide = int.Parse(data["width"].ToString());
    tilesHigh = int.Parse(data["height"].ToString());
  }

  public void LoadMap() {
    int firstGid = 1;
    foreach (Hashtable layer in (ArrayList)data["layers"]) {
      int tileIndex = 0;
      ArrayList mapData = (ArrayList)layer["data"];
      string layerName = layer["name"].ToString();

      for (int i = tilesWide; i > 0; i--) {
        for (int j = 0; j < tilesHigh; j++) {
          // This is already converted to game units
          // ie. 16px is 1 unit.
          int prefabKey = (Convert.ToInt32(mapData[tileIndex]));
          if (prefabKey != 0) {
            prefabKey -= firstGid;
          }
          string prefab = tilesetMap[prefabKey];

          GameObject obj = Resources.Load<GameObject>("Prefabs/" + prefab);

          // we'll return a null object if we run
          // into tiled's placeholder (tile id 0)
          if (obj != null) {
            GameObject mapTile = Instantiate(obj, new Vector3(j, i), Quaternion.identity) as GameObject;
            mapTile.renderer.sortingLayerName = layerName;
            mapTile.transform.parent = transform;
          }
         
          tileIndex += 1;
        }
      }
    }
  }

	void Start() {
    tilesetMap = new Dictionary<int, string>();
    tilesetMap.Add(0, ""); // Represents the empty Prefab

    var json = File.ReadAllText("Assets/Resources/Tilemaps/forest.json");
    data = (Hashtable)JSON.JsonDecode(json);

    SetTileData();
    PopulateTilesetMap();

    LoadMap();
  }
}
