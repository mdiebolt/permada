using UnityEngine;
using System.Collections;

namespace Player {
	public class Weapon : MonoBehaviour {
		private int damage = 1;

		void OnTriggerEnter2D(Collider2D collider) {
			if (collider.tag == "Enemy") {
				var enemy = collider.gameObject;

				enemy.SendMessage("TakeDamage", damage);
				enemy.SendMessage("KnockBack", transform.position);
			}
		}
	}
}