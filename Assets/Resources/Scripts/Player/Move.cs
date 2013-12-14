using UnityEngine;
using System.Collections;

namespace Player {
	public class Move : MonoBehaviour {
		private Vector3 velocity = new Vector3(1.0f, 0, 0);
		public Vector3 facing;
		private float speed = 6.0f;

		private bool swinging = false;
		private float swingFor = 0; // How many degrees to swing across
		private float swingFrom = 0; // Where to start swinging
		private int swungDegrees = 0;

		private void MovePlayer(Vector3 velocity) {
			var position = transform.position;
			var dt = Time.deltaTime;

			position += (dt * velocity);

			transform.position = position;
		}

		private void FlipWeapon(Vector3 velocity) {
			var arrow = transform.Find("Weapon");

			if (velocity.y > 0 && !swinging)
				arrow.rotation = Quaternion.Euler(0, 0, 90);
			else if (velocity.y < 0 && !swinging)
				arrow.rotation = Quaternion.Euler(0, 0, 270);

			if (velocity.x > 0 && !swinging)
				arrow.rotation = Quaternion.Euler(0, 0, 0);
			else if (velocity.x < 0 && !swinging)
				arrow.rotation = Quaternion.Euler(0, 0, 180);
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

//		private void UpdateAnimation() {
//			var animator = GetComponent<Animator>();
//
//			if (velocity.x > 0)
//				animator.Play("right_walk");
//			else if (velocity.x < 0)
//				animator.Play("left_walk");
//			else if (velocity.y > 0)
//				animator.Play("up_walk");
//			else if (velocity.y < 0)
//				animator.Play("down_walk");
//		}

		private void SetWeaponEnabled(bool value) {
			var weapon = GameObject.FindGameObjectWithTag("Weapon");

			weapon.renderer.enabled = value;
			weapon.GetComponent<BoxCollider2D>().enabled = value;
    	}

		void Swing() {
			var arrow = transform.Find("Weapon");
			var rot = arrow.eulerAngles;

			if (swungDegrees < swingFor) {
				swungDegrees += 5;
				rot.z += 5;
			} else {
				swinging = false;
				swungDegrees = 0;

				SetWeaponEnabled(false);
			}

			arrow.eulerAngles = rot;
		}

		void Update () {
			UpdateVelocity();
			UpdateFacing();
			MovePlayer(velocity);
			FlipWeapon(facing);
			//UpdateAnimation();

      if (Input.GetKeyDown("b")) {
        var bomb = Resources.Load<GameObject>("Prefabs/Bomb");
        Instantiate(bomb, transform.position, Quaternion.identity);
      }

      if (Input.GetKeyDown("a")) {
        var prefab = Resources.Load<GameObject>("Prefabs/Arrow");
        Instantiate(prefab, transform.position, Quaternion.identity);
      }

			if (Input.GetKeyDown("space")) {
				var arrow = transform.Find("Weapon");
				var rot = arrow.eulerAngles;

				swingFor = 90;
				swinging = true;

				SetWeaponEnabled(true);

				if (facing.x > 0)
					swingFrom = -45.0f;
				else if (facing.x < 0)
					swingFrom = 135.0f;
				else if (facing.y > 0)
					swingFrom = 45.0f;
				else if (facing.y < 0)
					swingFrom = 235.0f;

				rot.z = swingFrom;
				arrow.eulerAngles = rot;
			}

			if (swinging)
				Swing();
		}
	}
}