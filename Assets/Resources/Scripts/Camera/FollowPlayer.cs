using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Camera {
	public class FollowPlayer : MonoBehaviour {
		public float xSmooth = 16f;		
		public float ySmooth = 16f;	

    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

		private Transform player;

    private int cameraLeft = 0;
    private int cameraRight = 24;

    private int cameraTop = 24;
    private int cameraBottom = 0;

		void Start() {
			player = GameObject
        .FindGameObjectWithTag("Player")
        .transform;	
		}
		
		void FixedUpdate() {
			TrackPlayer();
		}

    private void updateCurrentPosition(int x, int y) {
      GameObject
        .Find("World")
        .GetComponent<Level.World>()
        .UpdateCurrentPosition(x, y);
    }

    private void moveCameraHorizontally(int direction) {
      var amount = direction * 24;
      
      cameraLeft += amount;
      cameraRight += amount;

      var midpoint = (cameraLeft + cameraRight) / 2;
      
      minX = midpoint - 1.5f;
      maxX = midpoint + 1.5f;

      updateCurrentPosition(direction, 0);
    }

    private void moveCameraVertically(int direction) {
      var amount = direction * 24;
      
      cameraTop += amount;
      cameraBottom += amount;

      var midpoint = (cameraTop + cameraBottom) / 2; 
      
      minY = midpoint - 6f; 
      maxY = midpoint + 6f;

      updateCurrentPosition(0, direction);
    }

    private void focusLeft() {
      moveCameraHorizontally(-1);
    }

    private void focusRight() {
      moveCameraHorizontally(1);
    }

    private void focusUp() {
      moveCameraVertically(1);
    }

    private void focusDown() {
      moveCameraVertically(-1);
    }

    private void switchAreas() {
      if (player.position.x < cameraLeft) {
        focusLeft();
      } else if (player.position.x > cameraRight) {
        focusRight();
      } else if (player.position.y < cameraBottom) {
        focusDown();
      } else if (player.position.y > cameraTop) {
        focusUp();
      } 
    }

    private void updateCameraPosition() {
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
      updateCameraPosition();
      switchAreas();
		}
	}
}