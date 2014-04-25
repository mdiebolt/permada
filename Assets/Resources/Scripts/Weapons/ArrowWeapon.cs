using UnityEngine;
using System.Collections;

public class ArrowWeapon : MonoBehaviour {
  private Vector3 velocity;
  private float speed = 10f;

  private int damage = 2;

  public void Awake() {
    var facing = GameObject
      .Find("Player")
      .GetComponent<PlayerMove>()
      .facing;

    velocity = facing * speed;
    rotate();
  }

  private void rotate() {
    if (velocity.x > 0) {
      transform.eulerAngles = new Vector3(0, 0, 0);
    } else if (velocity.x < 0) {
      transform.eulerAngles = new Vector3(0, 0, 0);
      transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
    } else if (velocity.y > 0) {
      transform.eulerAngles = new Vector3(0, 0, 90);
    } else {
      transform.eulerAngles = new Vector3(0, 0, 270);
    }
  }

	void Update() {
    transform.position += velocity * Time.deltaTime;
  }
 
  void OnTriggerEnter2D(Collider2D collider) {
    var obj = collider.gameObject;

    if (obj.tag == "Enemy" && obj.GetComponent<Damagable>()) {
      var baseDamage = GameObject.Find("Player")
        .GetComponent<PlayerAttack>()
        .BaseDamage;

      obj.SendMessage("Damage", damage + baseDamage);
      Destroy(gameObject);
    }
  }
}
