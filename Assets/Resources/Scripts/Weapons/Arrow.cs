using UnityEngine;
using System.Collections;
using Player;

namespace Weapons {
  public class Arrow : MonoBehaviour {
    private Vector3 velocity;
    private float speed = 15f;

    private int damage = 2;

    public void Start() {
      var facing = GameObject.Find("Player").GetComponent<Move>().facing;
      velocity = facing * speed;
    }

  	void Update() {
      if (velocity.x > 0)
        transform.eulerAngles = new Vector3(0, 0, 0);
      else if (velocity.x < 0)
        transform.eulerAngles = new Vector3(0, 0, 180);
      else if (velocity.y > 0)
        transform.eulerAngles = new Vector3(0, 0, 90);
      else
        transform.eulerAngles = new Vector3(0, 0, 270);

      transform.position += velocity * Time.deltaTime;
    }
   
    void OnTriggerEnter2D(Collider2D collider) {
      var obj = collider.gameObject;

      if (obj.tag == "Enemy" && obj.GetComponent<Damagable>()) {
        obj.SendMessage("Damage", damage);
        Destroy(gameObject);
      }
    }
  }
}