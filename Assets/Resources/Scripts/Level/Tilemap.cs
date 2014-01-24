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

  private void LoadMap() {
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
            // offset by 0.5 so the map snaps to the game grid
            GameObject mapTile = Instantiate(obj, new Vector3(j + 0.5f, i - 0.5f), Quaternion.identity) as GameObject;
            mapTile.renderer.sortingLayerName = layerName;
            mapTile.transform.parent = transform;
          }
         
          tileIndex += 1;
        }
      }
    }
  }

  public void LoadMap(string name) {
    var json = File.ReadAllText("Assets/Resources/Tilemaps/" + name + ".json");
    data = (Hashtable)JSON.JsonDecode(json);

    SetTileData();
    PopulateTilesetMap();
    
    LoadMap();
  }

	void Start() {
    tilesetMap = new Dictionary<int, string>();
    tilesetMap.Add(0, ""); // Represents the empty Prefab

    LoadMap("maze");
  }
}
