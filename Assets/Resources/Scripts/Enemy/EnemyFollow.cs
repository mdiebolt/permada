using UnityEngine;
using System.Collections;

public class EnemyFollow : MonoBehaviour {
	private float speed = 3.0f;
	private int health = 3;
	private SpriteRenderer spriteRenderer;
	private Vector3 velocity = new Vector3(0, 0, 0);
  private bool invulnerable = false;

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
		position += velocity * Time.deltaTime;

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
    invulnerable = true;

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

    invulnerable = false;
    color.a = 1.0f;
    spriteRenderer.color = color;
  }

  void Damage(int damage) {
    if (!invulnerable) {
		  health -= damage;
    
			if (health <= 0)
				Destroy(gameObject);

			StartCoroutine(Flicker());
    }
	}
}
