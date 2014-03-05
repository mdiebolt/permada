using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Player {
  public class Attack : MonoBehaviour {
    private List<Combo> combos = new List<Combo>();
    private List<string> attacks = new List<string>(7);
    private bool comboActive = false;

    void Start() {
      createCombos();
    }

    private void createCombos() {
      var arrowKeys = new List<string>() { "Arrow", "Arrow", "Arrow" };
      combos.Add(new Combo(arrowKeys));

      var bombKeys = new List<string>() { "Bomb", "Bomb", "Bomb" };
      combos.Add(new Combo(bombKeys));
    }

    private void attack(string type) {
      if (comboActive && attacks.Count < 7) {
        attacks.Add(type);

        foreach (var c in combos) {
          if (c.Matches(attacks)) {
            Debug.Log(c.Name + " combo executed");
          }
        }
      } else {
        var obj = Resources.Load<GameObject>("Prefabs/" + type);
        Instantiate(obj, transform.position, Quaternion.identity);
      }
    }

    private void clearCombo() {
      attacks.Clear();
    }

  	void Update() {
      if (Input.GetButton("Combo")) {
        comboActive = true;
      } else {
        comboActive = false;
        clearCombo();
      }

      if (Input.GetButtonDown("Bomb")) {
        attack("Bomb");
      }

      if (Input.GetButtonDown("Arrow")) {
        attack("Arrow");
      }
  	}
  }
}