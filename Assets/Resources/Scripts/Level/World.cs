﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {
  public static int AREA_WIDTH = 25;
  public static int AREA_HEIGHT = 25;

  private int width = 1;
  private int height = 1;
  private Vector2 currentPosition = Vector2.zero;
  private List<GameObject> areas = new List<GameObject>();

  private List<string> levels = new List<string> { 
    "forest", 
    "forest1", 
    "battle",
    "swoop",
    "dark_world"
  };
  
	void Start() {
    generate();
	}

  public void UpdateCurrentPosition(int x, int y) {
    var amount = new Vector2(x, y);
    currentPosition += amount;

    foreach (var area in areas) {
      // Disable the boss text label. The boss will enabled it when activated
      if (GameObject.Find("Label(Clone)")) {
        DestroyImmediate(GameObject.Find("Label(Clone)"));
      }

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

  private void placeBoss() {
    var index = Random.Range(0, areas.Count);
    var area = areas[index];

    var boss = Resources.Load<GameObject>("Prefabs/Boss");

    var obj = Instantiate(boss, Vector3.zero, Quaternion.identity) as GameObject;
    obj.transform.parent = area.transform;
    obj.transform.position = Vector3.zero;
  }

  private void generatePowerup(string type) {
    var count = Random.Range(1, 3);

    var prefabName = string.Format("Prefabs/{0}Serpin", type);
    var serpin = Resources.Load<GameObject>(prefabName);

    for (int i = 0; i < count; i++) {
      // TODO: better way to get absolute world bounds
      var x = Random.Range(-24f, 48f);
      var y = Random.Range(-24f, 48f);
      
      Instantiate(serpin, new Vector3(x, y, 0), Quaternion.identity);
    }
  }

  private void placePowerups() {
    generatePowerup("Attack");
    generatePowerup("Speed");
    generatePowerup("Defense");
  }

  private void generate() {
    for (int i = -width; i <= width; i++) {
      for (int j = -height; j <= height; j++) {
        var index = Random.Range(0, levels.Count);
        var level = levels[index];

        load(level, i, j);
      }
    }

    placeBoss();
    placePowerups();

    // deactivate all other areas
    UpdateCurrentPosition(0, 0);
  }
}
