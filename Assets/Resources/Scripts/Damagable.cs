﻿using UnityEngine;

public class Damagable : MonoBehaviour {
  public int health = 0;
  public int maxHealth = 5;

  private void updateUI() {
    if (GameObject.Find("Health")) {
      var obj = GameObject.Find("Health");
      obj.SendMessage("RedrawPlayerHealth");
    }
  }

  public void Heal(int amount) {
    if (health < maxHealth) {
      health += amount;
    }

    updateUI();
  }

  public void Damage(int damage) {
    health -= damage;
    updateUI();
    
    if (health <= 0) {
      Destroy(gameObject);
      if (gameObject.name == "Boss(Clone)") {
        Application.LoadLevel("Victory");
      } else if (gameObject.name == "Player") {
        Application.LoadLevel("Death");
      }
    }
  }
}
