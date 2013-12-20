using UnityEngine;

public class Damagable : MonoBehaviour {
  public int health = 0;
  public int maxHealth = 5;

  private void updateUI() {
    var obj = GameObject.Find("Health");
    obj.SendMessage("RedrawPlayerHealth");
  }

  void Heal(int amount) {
    if (health < maxHealth) {
      health += amount;
    }

    updateUI();
  }

  void Damage(int damage) {
    health -= damage;
    updateUI();
    
    if (health <= 0)
      Destroy(gameObject);
  }
}
