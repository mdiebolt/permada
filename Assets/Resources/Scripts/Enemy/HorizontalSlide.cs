using UnityEngine;
using System.Collections;

namespace Enemy {
  public class HorizontalSlide : MonoBehaviour {
    private Vector3 facing = Vector3.zero;
    private float speed = 5.0f;

    private void slideLeft() {
      facing = Vector3.left;
    }

    private void slideRight() {
      facing = Vector3.right;
    }

  	void Update() {
      facing = Vector3.zero;

      var start = transform.position;
      var left = Vector3.left;
      var right = Vector3.right;

      var layerMask = 1 << LayerMask.NameToLayer("Player");

      if (Physics2D.Raycast(start, right, Mathf.Infinity, layerMask)) {
        slideRight();
      }

      if (Physics2D.Raycast(start, left, Mathf.Infinity, layerMask)) {
        slideLeft();
      } 
    
      transform.position = transform.position + (facing * speed) * Time.deltaTime;
  	}
  }
}
