using UnityEngine;
using System.Collections;

namespace Enemy {
  public class Bullet : MonoBehaviour {
    private Vector3 direction = new Vector3(0, 0, 0); 
    private float speed = 5.0f;

  	void Update () {
      var rotation = transform.eulerAngles;
      rotation.z -= 5.0f;
       
      transform.eulerAngles = rotation;

      var position = transform.position;

      position += (direction * speed * Time.deltaTime);

      transform.position = position;
  	}

    void SetDirection(Vector3 dir) {
      direction = dir;
    }
  }
}