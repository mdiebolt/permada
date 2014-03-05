using UnityEngine;
using System.Collections;

public class Ghostable : MonoBehaviour {
  private CircleCollider2D circleCollider;
  private SpriteRenderer spriteRenderer;

  void Start() {
    circleCollider = gameObject.GetComponent<CircleCollider2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private IEnumerator countTo(float duration) {
    float elapsed = 0;
    
    while (elapsed < duration) {
      elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
      yield return null;
    }
  }

  private void opacity(float value) {
    var color = spriteRenderer.color;
    color.a = value;
    spriteRenderer.color = color;
  }

  private void ghost() {
    circleCollider.enabled = false;
    opacity(0.3f);
  }

  private void unghost() {
    circleCollider.enabled = true;
    opacity(1.0f);
  }

  public IEnumerator Ghost() {
    ghost();
    yield return StartCoroutine(countTo(10));
    unghost();
  }
}
