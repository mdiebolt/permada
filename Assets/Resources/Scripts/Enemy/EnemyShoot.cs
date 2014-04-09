using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShoot : MonoBehaviour {
  private float elapsedTime; 
  private GameObject boomerang;

  void Start() {
    boomerang = Resources.Load<GameObject>("Prefabs/Boomerang");
  }

  void Update() {
    elapsedTime += Time.deltaTime;

    if (elapsedTime > 5.0f) {
      fire();
      elapsedTime = 0;
    }
	}

  private void createShot(Vector3 direction) {
    var obj = Instantiate(boomerang, transform.position, Quaternion.identity) as GameObject;
    obj.GetComponent<EnemyBullet>().Direction = direction;
  }

  private void fire() {
    createShot(Vector3.up);
    createShot(Vector3.down);
    createShot(Vector3.left);
    createShot(Vector3.right);
  }
}
