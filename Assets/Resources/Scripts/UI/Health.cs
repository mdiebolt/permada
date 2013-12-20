using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
  private GameObject player;
  private Object heart;

  private void removeHearts() {
    foreach (Transform child in gameObject.GetComponentsInChildren<Transform>()) {
      if (child != transform) {
        Destroy(child.gameObject);
      }
    }
  }

  private void drawHearts() {
    removeHearts();

    var health = player.GetComponent<Damagable>().health;
  
    for (int i = 0; i < health; i++) {
      // draw hearts relative to heart container
      var position = new Vector3(0.035f + (i * 0.05f), 0.95f, 0);
      var obj = Instantiate(heart, position, Quaternion.identity) as GameObject;
      obj.transform.parent = transform;
    }
  }

	void Start() {
    player = GameObject.Find("Player");
    heart = Resources.Load("Prefabs/Heart");

    drawHearts();
  }

  void RedrawHealth() {
    drawHearts();
  }
}
