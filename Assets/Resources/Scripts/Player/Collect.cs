using UnityEngine;
using System.Collections;

namespace Player {
  public class Collect : MonoBehaviour {
    private int serpinCount = 0;

    void OnTriggerEnter2D(Collider2D collider) {
      var obj = collider.gameObject;
      
      if(collider.name == "Serpin") {
        Destroy(obj);
        serpinCount += 1;
        GetComponent<Move>().Speed += 1.0f;
      }
    }
  }
}