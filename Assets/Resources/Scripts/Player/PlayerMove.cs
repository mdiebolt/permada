using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
  private const float WORLD_MIN_X = -25f;
  private const float WORLD_MAX_X = 50f;

  private const float WORLD_MIN_Y = -25f;
  private const float WORLD_MAX_Y = 50f;

	private Vector3 velocity = Vector3.zero;
	public Vector3 facing = Vector3.down;
	public float Speed = 6.0f;

  private void clampToWorldBounds() {
    var position = transform.position;

    // 0.5 offsets are half the player sprite 
    if (position.x - 0.5 < WORLD_MIN_X) {
      position.x = WORLD_MIN_X + 0.5f;
    }

    if (position.x + 0.5 > WORLD_MAX_X) {
      position.x = WORLD_MAX_X - 0.5f;
    }

    if (position.y - 0.5 < WORLD_MIN_Y) {
      position.y = WORLD_MIN_Y + 0.5f;
    }

    if (position.y + 0.5 > WORLD_MAX_Y) {
      position.y = WORLD_MAX_Y - 0.5f;
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
