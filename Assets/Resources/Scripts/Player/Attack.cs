using UnityEngine;
using System.Collections;

namespace Player {
  public class Attack : MonoBehaviour {
    private IEnumerator CountTo(float duration) {
      float elapsed = 0;
      
      while (elapsed < duration) {
        elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
        yield return null;
      }
    }

    private IEnumerator ghost() {
      var collider = gameObject.GetComponent<CircleCollider2D>();
      collider.enabled = false;

      var spriteRenderer = GetComponent<SpriteRenderer>();
      var color = spriteRenderer.color;
      color.a = 0.3f;

      spriteRenderer.color = color;

      yield return StartCoroutine(CountTo(10));

      collider.enabled = true;

      color.a = 1.0f;
      spriteRenderer.color = color;
    }

    private IEnumerator bigHitPause() {
      int elapsed = 0;
      int duration = 30;

      Time.timeScale = 0;

      while (elapsed < duration) {
        elapsed += 1; 
        yield return null;
      }

      Time.timeScale = 1.0f;
    }

  	void Update() {
      if (Input.GetButtonDown("Bomb")) {
        var bomb = Resources.Load<GameObject>("Prefabs/Bomb");
        Instantiate(bomb, transform.position, Quaternion.identity);
      }

      if (Input.GetButtonDown("Arrow")) {
        var prefab = Resources.Load<GameObject>("Prefabs/Arrow");
        Instantiate(prefab, transform.position, Quaternion.identity);
      }

      if (Input.GetKeyDown("g")) {
        var grave = GameObject.Find("Grave");
        var distance = Vector3.Distance(grave.transform.position, transform.position);

        if (distance < 1.25f) {
          StartCoroutine(ghost());
        }
      }

      if (Input.GetKeyDown("t")) {
        StartCoroutine(bigHitPause());
      }
  	}
  }
}