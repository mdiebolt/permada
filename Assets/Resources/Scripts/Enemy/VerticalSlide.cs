using UnityEngine;
using System.Collections;

public class VerticalSlide : MonoBehaviour {
  private Vector3 facing = Vector3.zero;
  private float speed = 5.0f;
  
  private void slideUp() {
    facing = Vector3.up;
  }
  
  private void slideDown() {
    facing = Vector3.down;
  }
  
  private void move() {
    transform.position = transform.position + (facing * speed) * Time.deltaTime;
  }
  
  private void checkForPlayer() {
    var start = transform.position;
    var layerMask = 1 << LayerMask.NameToLayer("Player");
    
    if (Physics2D.Raycast(start, Vector3.up, Mathf.Infinity, layerMask)) {
      slideUp();
    }
    
    if (Physics2D.Raycast(start, Vector3.down, Mathf.Infinity, layerMask)) {
      slideDown();
    } 
  }
  
  void Update() {
    facing = Vector3.zero;
    
    checkForPlayer();
    move();
  }
}
