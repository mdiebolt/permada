using UnityEngine;
using System.Collections;

namespace Enemy {
	public class Move : MonoBehaviour {
		private float speed = 0.05f;
		private int health = 3;
		private SpriteRenderer spriteRenderer;
		private CircleCollider2D circleCollider;
		private Vector3 velocity = new Vector3(0, 0, 0);

		void Start() {
			spriteRenderer = GetComponent<SpriteRenderer>();
			circleCollider = GetComponent<CircleCollider2D>();
		}

		private Vector3 PlayerPosition() {
			return GameObject.FindGameObjectWithTag("Player").transform.position;
		}

		private Vector3 CalculateDistance() {
			return (PlayerPosition() - transform.position).normalized;
		}

		private void MoveEnemy() {
			var position = transform.position;
			position += velocity;

			transform.position = position;
		}

		void Update() {
			var distance = CalculateDistance();

			velocity = distance * speed;

			MoveEnemy();
		}

    private IEnumerator Flicker() {
      float elapsed = 0;
      float duration = 2;
      int frameCount = 0;

      var color = spriteRenderer.color;
      circleCollider.enabled = false;

      while (elapsed < duration) {
        elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
        frameCount += 1;

				if (frameCount % 10 == 0) {
	        color.a = 1.0f;
	        spriteRenderer.color = color;
	      } else {
	        color.a = 0.5f;
	        spriteRenderer.color = color;
	      }

        yield return null;
      }

      circleCollider.enabled = true;
      color.a = 1.0f;
      spriteRenderer.color = color;
    }

    void Damage(int damage) {
			health -= damage;

			if (health <= 0)
				Destroy(gameObject);

			StartCoroutine(Flicker());
		}
	}
}