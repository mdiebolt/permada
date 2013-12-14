using UnityEngine;
using System.Collections;

namespace Player {
	public class Weapon : MonoBehaviour {
		private int damage = 1;

		void OnTriggerEnter2D(Collider2D collider) {
			var obj = collider.gameObject;

      if (obj.GetComponent<Damagable>())
			  obj.SendMessage("Damage", damage);

      if (obj.GetComponent<Knockable>())
			  obj.SendMessage("KnockBack", transform.position);
		}
	}
}