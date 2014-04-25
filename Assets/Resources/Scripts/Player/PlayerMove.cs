using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
  private float worldMinX = -(float)World.AREA_WIDTH;
  private float worldMaxX = (float)(2 * World.AREA_WIDTH);

  private float worldMinY = -(float)World.AREA_HEIGHT;
  private float worldMaxY = (float)(2 * World.AREA_WIDTH);

	private Vector3 velocity = Vector3.zero;
	public Vector3 facing = Vector3.down;
	public float Speed = 6.0f;

  private void clampToWorldBounds() {
    var position = transform.position;

    // 0.5 offsets are half the player sprite 
    if (position.x - 0.5 < worldMinX) {
      position.x = worldMinX + 0.5f;
    }

    if (position.x + 0.5 > worldMaxX) {
      position.x = worldMaxX - 0.5f;
    }

    if (position.y - 0.5 < worldMinY) {
      position.y = worldMinY + 0.5f;
    }

    if (position.y + 0.5 > worldMaxY) {
      position.y = worldMaxY - 0.5f;
    }

    transform.position = position;
  }

	private void movePlayer() {
		var position = transform.position;

    position += (velocity * Time.deltaTime);
    transform.position = position;

    clampToWorldBounds();
  }

	private void updateFacing() {
    var horizontal = Input.GetAxis("Horizontal");
    var vertical = Input.GetAxis("Vertical");
		
    if(Mathf.Abs(horizontal) > Mathf.Abs(vertical)) {
      if (horizontal > 0) {
        facing = Vector3.right; 
      } else {
        facing = Vector3.left;
      }
		} else if (Mathf.Abs(vertical) > Mathf.Abs(horizontal)) {
      if (vertical > 0) {
        facing = Vector3.up;
      } else {
        facing = Vector3.down;
      }
    }
	}

	private void updateVelocity() {
    velocity = Vector3.zero;

    velocity.x = Input.GetAxis("Horizontal") * Speed;
    velocity.y = Input.GetAxis("Vertical") * Speed;
	}
 
	void Update() {
		updateVelocity();
		updateFacing();
		movePlayer();	
	}
}
