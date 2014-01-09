using UnityEngine;
using System.Collections;

namespace Weapons {
  public class Bomb : MonoBehaviour {
    private int damage = 5;

    private void AlternateColor() {
      if (renderer.material.color.Equals(Color.red)) 
        renderer.material.color = Color.white;
      else
        renderer.material.color = Color.red;
    }

  	private IEnumerator Blink() {
      float elapsed = 0;
      float duration = 4;
      float changeEvery = 0;

      while (elapsed < duration) {
        elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
        changeEvery = Mathf.MoveTowards(changeEvery, 1, Time.deltaTime);

        if (changeEvery > 0.5) {
          changeEvery = 0;
          AlternateColor();
        }

        yield return null;
      }

      StartCoroutine(Explode());
    }

    private IEnumerator CountTo(float duration) {
      float elapsed = 0;

      while (elapsed < duration) {
        elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
        yield return null;
      }
    }

    private IEnumerator Explode() {
      float duration = 1;

      var damageArea = gameObject.GetComponent<CircleCollider2D>();
      damageArea.enabled = true;

      yield return StartCoroutine(CountTo(duration));

      Destroy(gameObject);
    }

    void Start() {
      StartCoroutine(Blink());
    }

    void OnTriggerEnter2D(Collider2D collider) {
      var obj = collider.gameObject;

      if (obj.GetComponent<Damagable>())
        obj.SendMessage("Damage", damage);
    }
  }
}
