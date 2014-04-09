using UnityEngine;
using System.Collections;

public class PlayerCollect : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D collider) {
    var obj = collider.gameObject;

    if(collider.name == "SpeedSerpin(Clone)") {
      Destroy(obj);
      GetComponent<PlayerMove>().Speed += 2.0f;
    } else if (collider.name == "AttackSerpin(Clone)") {
      Destroy(obj);
      GameObject.Find("Player")
        .GetComponent<PlayerAttack>()
        .BaseDamage += 2;
    } else if (collider.name == "DefenseSerpin(Clone)") {
      Destroy(obj);
      GameObject.Find("Player")
        .GetComponent<Damagable>()
        .Heal(1);
    }
  }
}
