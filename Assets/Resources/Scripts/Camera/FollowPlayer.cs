using UnityEngine;
using System.Collections;

namespace Camera {
	public class FollowPlayer : MonoBehaviour {
		public float xMargin = 1f;		
		public float yMargin = 1f;		

		public float xSmooth = 32f;		
		public float ySmooth = 32f;		

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
			
			//targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
			//targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);
			
			transform.position = new Vector3(targetX, targetY, transform.position.z);
		}
	}
}