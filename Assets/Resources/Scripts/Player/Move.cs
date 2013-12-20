using UnityEngine;
using System.Collections;

namespace Player {
	public class Move : MonoBehaviour {
		private Vector3 velocity = new Vector3(1.0f, 0, 0);
		public Vector3 facing;
		private float speed = 6.0f;
    private int serpinCount = 0;

		private void MovePlayer() {
			var position = transform.position;

			position += (velocity * Time.deltaTime);
			transform.position = position;
		}

		private void UpdateFacing() {
			if(Input.GetKey(KeyCode.LeftArrow)) {
				facing.x = -1.0f;
				facing.y = 0;
			}
			
			if(Input.GetKey(KeyCode.RightArrow)) {
				facing.x = 1.0f;
				facing.y = 0;
			}

			if(Input.GetKey(KeyCode.UpArrow)) {
				facing.x = 0;
				facing.y = 1.0f;
			}
				
			if(Input.GetKey(KeyCode.DownArrow)) {
				facing.x = 0;
				facing.y = -1.0f;
			}
		}

		private void UpdateVelocity() {
			velocity.x = 0;
			velocity.y = 0;
			
			if(Input.GetKey(KeyCode.LeftArrow))
				velocity.x = -speed;
			
			if(Input.GetKey(KeyCode.RightArrow)) 
				velocity.x = speed;

			if(Input.GetKey(KeyCode.UpArrow))
				velocity.y = speed;

			if(Input.GetKey(KeyCode.DownArrow))
				velocity.y = -speed;
		}
   
		void Update() {
			UpdateVelocity();
			UpdateFacing();
			MovePlayer();	
		}

    void OnTriggerEnter2D(Collider2D collider) {
      var obj = collider.gameObject;

      if(collider.name == "Serpin") {
        Destroy(obj);
        serpinCount += 1;
        Debug.Log(serpinCount);
      }
    }
	}
}