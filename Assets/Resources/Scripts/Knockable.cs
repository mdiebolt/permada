using UnityEngine;

public class Knockable : MonoBehaviour {
  void KnockBack(Vector3 sourcePosition) {
    var position = transform.position;
    var knockbackDirection = (position - sourcePosition).normalized;
    
    StartCoroutine(transform.MoveTo((position + knockbackDirection) * 1.5f, 0.25f));
  }
}
