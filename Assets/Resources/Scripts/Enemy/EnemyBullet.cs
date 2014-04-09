using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {
  public Vector3 Direction = Vector3.right;
  private float speed = 5.0f;
  private int amount = 1;

	void Update() {
    var rotation = transform.eulerAngles;
    rotation.z -= 100.0f * Time.deltaTime;
     
    transform.eulerAngles = rotation;

    var position = transform.position;
    position += (Direction * speed * Time.deltaTime);
    transform.position = position;
	}

  void OnTriggerEnter2D(Collider2D collider) {
    var obj = collider.gameObject;

    if (obj.tag == "Player") {
      var damagable = obj.GetComponent<Damagable>();
      if (damagable) {
        damagable.Damage(amount);
        Destroy(gameObject);
      }
    }
  }
}
