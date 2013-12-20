using UnityEngine;

public class Damagable : MonoBehaviour {
  public int health = 0;

  private void updateUI() {
    var obj = GameObject.Find("Health");
    obj.SendMessage("RedrawHealth");
  }

  void Damage(int damage) {
    health -= damage;
    
    if (health <= 0)
      Destroy(gameObject);
  }

  void Update() {
    if(Input.GetKeyDown(KeyCode.Plus)) {
      health += 1;
      updateUI();
    }

    if(Input.GetKeyDown(KeyCode.Minus)) {
      health -= 1;
      updateUI();
    }
  }
}
