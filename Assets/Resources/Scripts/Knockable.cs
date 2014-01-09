using UnityEngine;

public class Knockable : MonoBehaviour {
  private float amount = 2.0f;
  private float duration = 0.25f;

  void KnockBack(Vector3 sourcePosition) {
    var position = transform.position;
    var knockbackDirection = (position - sourcePosition).normalized;
    var end = position + (knockbackDirection * amount);

    StartCoroutine(transform.MoveTo(end, duration));
  }
}
