using UnityEngine;
using System.Collections;

namespace Level {
	public class Destroy : MonoBehaviour {
		private int health = 0;

		void TakeDamage(int damage) {
			health -= damage;
			
			if (health <= 0)
				Destroy(gameObject);
		}

		void KnockBack(Vector3 sourcePosition) {
			;
		}
	}
}	