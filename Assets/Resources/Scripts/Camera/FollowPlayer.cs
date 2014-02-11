using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Level;

namespace Camera {
	public class FollowPlayer : MonoBehaviour {
		public float xMargin = 1f;		
		public float yMargin = 1f;		

		public float xSmooth = 16f;		
		public float ySmooth = 16f;	

		public World world;

    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

		private Transform player;

    private Vector3 currentLocation;

    private int cameraLeft = 0;
    private int cameraRight = 24;

    private int cameraTop = 24;
    private int cameraBottom = 0;

		void Start() {
      currentLocation = new Vector3(0, 0);
			player = GameObject.FindGameObjectWithTag("Player").transform;	
		}
		
		void FixedUpdate() {
			TrackPlayer();
		}

		private bool IsOutsideXMargin() {
			return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
		}
		
		private bool IsOutsideYMargin() {
			return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
		}

    private void SwitchAreas() {
      if (player.position.x < cameraLeft) {
        cameraLeft -= 24;
        cameraRight -= 24;

        currentLocation = new Vector3(currentLocation.x - 1, currentLocation.y);

        var midpoint = (cameraLeft + cameraRight) / 2;
        
        minX = midpoint - 1.5f; 
        maxX = midpoint + 1.5f; 
      } else if (player.position.x > cameraRight) {
        cameraLeft += 24;
        cameraRight += 24;

        currentLocation = new Vector3(currentLocation.x + 1, currentLocation.y);

        var midpoint = (cameraLeft + cameraRight) / 2;

        minX = midpoint - 1.5f; 
        maxX = midpoint + 1.5f; 
      } else if (player.position.y < cameraBottom) {
        cameraTop -= 24;
        cameraBottom -= 24;

        currentLocation = new Vector3(currentLocation.x, currentLocation.y - 1);

        var midpoint = (cameraTop + cameraBottom) / 2;

        minY = midpoint - 6f; 
        maxY = midpoint + 6f; 
      } else if (player.position.y > cameraTop) {
        cameraTop += 24;
        cameraBottom += 24;

        currentLocation = new Vector3(currentLocation.x, currentLocation.y + 1);

        var midpoint = (cameraTop + cameraBottom) / 2;

        minY = midpoint - 6f; 
        maxY = midpoint + 6f;
      } 

      LoadMap();
    }

    private void LoadMap() {
      // TODO switch active area on map
      //world.Load((int)currentLocation.x, (int)currentLocation.y);
    }

    private void UpdateCameraPosition() {
      var targetX = transform.position.x;
      var targetY = transform.position.y;
      
      var playerX = player.position.x;
      var playerY = player.position.y;
      
      targetX = Mathf.Lerp(targetX, playerX, xSmooth * Time.deltaTime);
      targetY = Mathf.Lerp(targetY, playerY, ySmooth * Time.deltaTime);

      targetX = Mathf.Clamp(targetX, minX, maxX);
      targetY = Mathf.Clamp(targetY, minY, maxY); 

      transform.position = new Vector3(targetX, targetY, transform.position.z);
    }

		void TrackPlayer() {
      UpdateCameraPosition();
      SwitchAreas();
		}
	}
}