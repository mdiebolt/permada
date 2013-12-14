using UnityEngine;

public class Damagable : MonoBehaviour {
  public int health = 0;
  
  void Damage(int damage) {
    health -= damage;
    
    if (health <= 0)
      Destroy(gameObject);
  }
}
