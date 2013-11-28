using UnityEngine;
using System.Collections;
using Level;

namespace Camera {
	public class FollowPlayer : MonoBehaviour {
		public float xMargin = 1f;		
		public float yMargin = 1f;		

		public float xSmooth = 16f;		
		public float ySmooth = 16f;	

		public Generate mapGenerator;

		public Vector2 maxXAndY;		
		public Vector2 minXAndY;		

		private Transform player;

		void Start () {
			player = GameObject.FindGameObjectWithTag("Player").transform;	
		}
		
		void FixedUpdate () {
			TrackPlayer();
		}

		bool IsOutsideXMargin() {
			return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
		}
		
		
		bool IsOutsideYMargin() {
			return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
		}

		void TrackPlayer() {
			float targetX = transform.position.x;
			float targetY = transform.position.y;

			targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
		
			targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
			targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);
			// If our view box is touching a map chunk edge, keep it there, don't let it go further
			// unless the player is past the chunk edge, then snap (quick lerp) to over there
//			if(player.transform.x < 4) {
//				;
//			}
			
			transform.position = new Vector3(targetX, targetY, transform.position.z);
		}
	}
}