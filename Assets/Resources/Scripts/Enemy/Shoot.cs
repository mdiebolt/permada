using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enemy;

namespace Enemy {
  public class Shoot : MonoBehaviour {
    private float elapsedTime; 
    private GameObject boomerang;

    void Start() {
      boomerang = Resources.Load<GameObject>("Prefabs/Boomerang");
    }

    void Update() {
      elapsedTime += Time.deltaTime;

      if (elapsedTime > 5.0f) {
        Fire();
        elapsedTime = 0;
      }
  	}

    private void createShot(Vector3 dir) {
      var obj = Instantiate(boomerang, transform.position, Quaternion.identity) as GameObject;
      obj.SendMessage("SetDirection", dir);
    }

    void Fire() {
      createShot(new Vector3(0, 1, 0));
      createShot(new Vector3(0, -1, 0));
      createShot(new Vector3(1, 0, 0));
      createShot(new Vector3(-1, 0, 0));
    }
  }
}