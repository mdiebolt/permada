using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Procurios.Public;
using System.IO;
using System;

namespace Level {
  public class Tilemap { 
    // We need to offset our lookups by 
    // using Tiled's first gid
    private const int TILED_FIRST_GID = 1;

    private static Dictionary<int, string> PopulateTilesetMap(Hashtable data) {
      Dictionary<int, string> tilesetMap = new Dictionary<int, string>();
      tilesetMap.Add(0, "");

      foreach (Hashtable set in (ArrayList)data["tilesets"]) {
        Hashtable properties = (Hashtable)set["tileproperties"];
        foreach (DictionaryEntry pair in properties) {
          Hashtable value = (Hashtable)pair.Value;

          tilesetMap.Add(int.Parse(pair.Key.ToString()), value["Prefab"].ToString());
        }
      }

      return tilesetMap;
    }

    private static GameObject prefab(ArrayList mapData, Dictionary<int, string> tilesetMap, int index) {
      int prefabKey = (Convert.ToInt32(mapData[index]));
      if (prefabKey != 0) {
        prefabKey -= TILED_FIRST_GID;
      }
      string prefabName = tilesetMap[prefabKey];
      
      return Resources.Load<GameObject>("Prefabs/" + prefabName);
    }

    private static Hashtable decode(string name) {
      var json = File.ReadAllText("Assets/Resources/Tilemaps/" + name + ".json");
      return (Hashtable)JSON.JsonDecode(json);
    }

    private static void add(GameObject obj, int i, int j, List<Tile> tiles) {
      // we'll return a null object if we run
      // into tiled's placeholder (tile id 0)
      if (obj != null) {
        var position = new Vector3(j + 0.5f, i - 0.5f);
        tiles.Add(new Tile(position, obj));
      } 
    }
    
    public static List<Tile> Load(string name) {
      List<Tile> tiles = new List<Tile>();
      var data = decode(name);
      Dictionary<int, string> tilesetMap = PopulateTilesetMap(data);

      foreach (Hashtable layer in (ArrayList)data["layers"]) {
        int tileIndex = 0;
        ArrayList mapData = (ArrayList)layer["data"];

        // This is already converted to game units
        // ie. 16px is 1 unit.
        for (int i = int.Parse(data["width"].ToString()); i > 0; i--) {
          for (int j = 0; j < int.Parse(data["height"].ToString()); j++) {
            GameObject obj = prefab(mapData, tilesetMap, tileIndex);

            add(obj, i, j, tiles);
            tileIndex += 1;
          }
        }
      }

      return tiles;
    }
  }
}
