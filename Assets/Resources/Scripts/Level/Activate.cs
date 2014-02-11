using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Level {
  public class Activate : MonoBehaviour {
    public bool activated = false;
    public Vector3 rewardLocation;

    void OnTriggerEnter2D(Collider2D collider) {
      var spriteRenderer = GetComponent<SpriteRenderer>();
      spriteRenderer.color = Color.red;
      activated = true;

      destroyIfActivated();
    }

    private void destroyIfActivated() {
      // TODO: scope this to the current map
      var triggers = GameObject.FindGameObjectsWithTag("Trigger");
      var allActive = triggers.All(t => t.GetComponent<Activate>().activated);

      if(allActive) {
        foreach (var t in triggers) {
          Destroy(t);
        }

        var obj = Resources.Load("Prefabs/Serpin");
        Instantiate(obj, rewardLocation, Quaternion.identity);
      }
    }
  }
}