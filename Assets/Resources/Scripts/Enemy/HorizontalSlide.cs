using UnityEngine;
using System.Collections;

public class HorizontalSlide : MonoBehaviour {
  private Vector3 facing = Vector3.zero;
  private float speed = 5.0f;

  private void slideLeft() {
    facing = Vector3.left;
  }

  private void slideRight() {
    facing = Vector3.right;
  }

  private void move() {
    transform.position = transform.position + (facing * speed) * Time.deltaTime;
  }

  private void checkForPlayer() {
    var start = transform.position;
    var layerMask = 1 << LayerMask.NameToLayer("Player");
    
    if (Physics2D.Raycast(start, Vector3.right, Mathf.Infinity, layerMask)) {
      slideRight();
    }
    
    if (Physics2D.Raycast(start, Vector3.left, Mathf.Infinity, layerMask)) {
      slideLeft();
    } 
  }

	void Update() {
    facing = Vector3.zero;

    checkForPlayer();
    move();
	}
}
