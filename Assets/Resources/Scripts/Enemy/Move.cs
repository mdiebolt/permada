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

			if (flickerCooldown % 5 == 0) 
				spriteRenderer.color = new Color(color.r, color.g, color.b, 1.0f);
			else
				spriteRenderer.color = new Color(color.r, color.g, color.b, 0.5f);
		}

		void TakeDamage(int damage) {
			health -= damage;

			flickerCooldown = 70;

			if (health <= 0)
				Destroy(gameObject);
		}

		void KnockBack(Vector3 sourcePosition) {
			var position = transform.position;
			var knockbackDistance = (position - sourcePosition) / 2;

			var dust = gameObject.GetComponent<ParticleSystem>();
			dust.Play();
			dust.enableEmission = true;
			dust.renderer.sortingLayerName = "UI";
			dust.renderer.sortingOrder = 5;

			StartCoroutine(transform.MoveTo(knockbackDistance, 0.25f));
		}
	}
}