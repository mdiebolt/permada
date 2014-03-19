using UnityEngine;
using System.Collections;

namespace Player {
	public class Move : MonoBehaviour {
		private Vector3 velocity = Vector3.zero;
		public Vector3 facing = Vector3.down;
		public float Speed = 6.0f;

		private void MovePlayer() {
			var position = transform.position;

			position += (velocity * Time.deltaTime);
			transform.position = position;
		}

		private void UpdateFacing() {
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

		private void UpdateVelocity() {
      velocity = Vector3.zero;

      velocity.x = Input.GetAxis("Horizontal") * Speed;
      velocity.y = Input.GetAxis("Vertical") * Speed;
		}
   
		void Update() {
			UpdateVelocity();
			UpdateFacing();
			MovePlayer();	
		}
	}
}