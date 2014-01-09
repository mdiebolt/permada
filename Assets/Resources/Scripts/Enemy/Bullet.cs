using UnityEngine;
using System.Collections;

namespace Enemy {
  public class Bullet : MonoBehaviour {
    private Vector3 direction = new Vector3(0, 0, 0); 
    private float speed = 5.0f;
    private int damage = 1;
    private bool reversed = false;

  	void Update() {
      var rotation = transform.eulerAngles;
      rotation.z -= 100.0f * Time.deltaTime;
       
      transform.eulerAngles = rotation;

      var position = transform.position;

      position += (direction * speed * Time.deltaTime);

      transform.position = position;
  	}

    private void reverseDirection() {
      reversed = true; 
      direction *= -1;
    }

    void SetDirection(Vector3 dir) {
      direction = dir;
    }

    void OnTriggerEnter2D(Collider2D collider) {
      var obj = collider.gameObject;

      if (obj.tag == "Player") {
        if (obj.GetComponent<Damagable>()) {
          obj.SendMessage("Damage", damage);
          Destroy(gameObject);
        }
      } else if (obj.tag == "Weapon") {
        if (!reversed) {
          reverseDirection();
        }
      }
    }
  }
}