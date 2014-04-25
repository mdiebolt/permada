using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossWizardShoot : MonoBehaviour {
  private float elapsedTime; 
  private GameObject boomerang;
  private int attackIndex = 0;
  private string[] attacks = new string[] { 
    "ex",
    "teleport",
    "plus",
    "spiral"
  };

  void Start() {
    boomerang = Resources.Load<GameObject>("Prefabs/Boomerang");
  }
  
  void Update() {
    elapsedTime += Time.deltaTime;

    if (!GameObject.Find("Label(Clone)")) {
      var label = Resources.Load<GameObject>("Prefabs/Label");
      Instantiate(label, new Vector3(0.8f, 0.96f, 0), Quaternion.identity);
    }

    if (elapsedTime > 2.0f) {
      attackIndex = (attackIndex + 1) % attacks.Length;
      SendMessage(attacks[attackIndex]);

      elapsedTime = 0;
    }
  }

  private void createShot(Vector3 direction) {
    var obj = Instantiate(boomerang, transform.position, Quaternion.identity) as GameObject;
    obj.GetComponent<EnemyBullet>().Direction = direction;
  }

  private void teleport() {
    var position = GameObject.Find("Player").transform.position;

    var x = Random.Range(position.x - 4, position.x + 4);
    var y = Random.Range(position.y - 4, position.y + 4);

    transform.position = new Vector3(x, y);
  }

  private void spiral() {
    for (float i = 0; i < (2 * Mathf.PI); i += (Mathf.PI / 15.0f)) {
      var vec = new Vector3(Mathf.Cos(i), Mathf.Sin(i));
      createShot(vec);
    }
  }

  private void ex() {
    createShot(new Vector3(1, 1));
    createShot(new Vector3(-1, 1));
    createShot(new Vector3(-1, -1));
    createShot(new Vector3(1, -1));
  }

  private void plus() {
    createShot(Vector3.up);
    createShot(Vector3.down);
    createShot(Vector3.left);
    createShot(Vector3.right);
  }
}
