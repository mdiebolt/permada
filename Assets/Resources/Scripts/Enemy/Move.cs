using UnityEngine;
using System.Collections;

namespace Enemy {
	public class Move : MonoBehaviour {
		private float speed = 0.05f;
		private int health = 3;
		private SpriteRenderer spriteRenderer;
		private int flickerCooldown = 0;
		private Vector3 velocity = new Vector3(0, 0, 0);

		void Start() {
			spriteRenderer = GetComponent<SpriteRenderer>();
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
			Flicker();
		}

		void Flicker() {
			flickerCooldown = Mathf.Max(flickerCooldown - 1, 0);

			var color = spriteRenderer.color;

			if (flickerCooldown % 5 == 0) {
        color.a = 1.0f;
        spriteRenderer.color = color;
      } else {
        color.a = 0.5f;
        spriteRenderer.color = color;
      }
    }

    void Damage(int damage) {
			health -= damage;

			flickerCooldown = 70;

			if (health <= 0)
				Destroy(gameObject);
		}
	}
}