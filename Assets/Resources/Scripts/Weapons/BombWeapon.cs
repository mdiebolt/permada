using UnityEngine;
using System.Collections;

public class BombWeapon : MonoBehaviour {
  private int damage = 10;

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
    var camera = GameObject.FindWithTag("MainCamera");
    StartCoroutine(camera.transform.Shake(0.25f, 0.25f));

    var damageArea = gameObject.GetComponent<CircleCollider2D>();
    damageArea.enabled = true;

    yield return StartCoroutine(CountTo(1));

    Destroy(gameObject);
  }

  void Start() {
    StartCoroutine(Blink());
  }

  void OnTriggerEnter2D(Collider2D collider) {
    var obj = collider.gameObject;

    if (obj.GetComponent<Damagable>()) {
      var baseDamage = GameObject.Find("Player")
        .GetComponent<PlayerAttack>()
        .BaseDamage;

      if (obj.name != "Player") {
        obj.SendMessage("Damage", damage * baseDamage);
      }
    }
  }
}
