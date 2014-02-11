using UnityEngine;
using System.Collections;

namespace Player {
	public class Move : MonoBehaviour {
		private Vector3 velocity = new Vector3(1.0f, 0, 0);
		public Vector3 facing;
		public float Speed = 6.0f;

		private void MovePlayer() {
			var position = transform.position;

			position += (velocity * Time.deltaTime);
			transform.position = position;
		}

		private void UpdateFacing() {
			if(Input.GetKey(KeyCode.LeftArrow)) {
        facing = Vector3.left;
			}
			
			if(Input.GetKey(KeyCode.RightArrow)) {
        facing = Vector3.right;
			}

			if(Input.GetKey(KeyCode.UpArrow)) {
        facing = Vector3.up;
			}
				
			if(Input.GetKey(KeyCode.DownArrow)) {
        facing = Vector3.down;
			}
		}

		private void UpdateVelocity() {
      velocity = Vector3.zero;
			
			if(Input.GetKey(KeyCode.LeftArrow))
				velocity.x = -Speed;
			
			if(Input.GetKey(KeyCode.RightArrow)) 
				velocity.x = Speed;

			if(Input.GetKey(KeyCode.UpArrow))
				velocity.y = Speed;

			if(Input.GetKey(KeyCode.DownArrow))
				velocity.y = -Speed;
		}
   
		void Update() {
			UpdateVelocity();
			UpdateFacing();
			MovePlayer();	
		}
	}
}