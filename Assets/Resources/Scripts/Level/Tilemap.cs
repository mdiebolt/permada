using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Procurios.Public;
using System.IO;
using System;

namespace Level {
  public class Tilemap { 
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

    public static List<Tile> Load(string name) {
      List<Tile> tiles = new List<Tile>();

      var json = File.ReadAllText("Assets/Resources/Tilemaps/" + name + ".json");
      var data = (Hashtable)JSON.JsonDecode(json);

      var tilesWide = int.Parse(data["width"].ToString());
      var tilesHigh = int.Parse(data["height"].ToString());

      Dictionary<int, string> tilesetMap = PopulateTilesetMap(data);

      int firstGid = 1;
      foreach (Hashtable layer in (ArrayList)data["layers"]) {
        int tileIndex = 0;
        ArrayList mapData = (ArrayList)layer["data"];

        for (int i = tilesWide; i > 0; i--) {
          for (int j = 0; j < tilesHigh; j++) {
            // This is already converted to game units
            // ie. 16px is 1 unit.
            int prefabKey = (Convert.ToInt32(mapData[tileIndex]));
            if (prefabKey != 0) {
              prefabKey -= firstGid;
            }
            string prefabName = tilesetMap[prefabKey];
            
            GameObject obj = Resources.Load<GameObject>("Prefabs/" + prefabName);
            
            // we'll return a null object if we run
            // into tiled's placeholder (tile id 0)
            if (obj != null) {
              var position = new Vector3(j + 0.5f, i - 0.5f);
              tiles.Add(new Tile(position, obj));
            }
            
            tileIndex += 1;
          }
        }
      }

      return tiles;
    }
  }
}
