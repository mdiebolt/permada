using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour {
  private List<PlayerCombo> combos = new List<PlayerCombo>();
  private List<string> attacks = new List<string>(7);
  private bool comboActive = false;
  public int BaseDamage = 1;

  void Start() {
    createCombos();
  }

  private void createCombos() {
    var arrowKeys = new List<string>() { "Arrow", "Arrow", "Arrow" };
    var arrowCombo = new PlayerCombo(arrowKeys);
    combos.Add(arrowCombo);

    var bombKeys = new List<string>() { "Bomb", "Bomb", "Bomb" };
    var bombCombo = new PlayerCombo(bombKeys);
    combos.Add(bombCombo);
  }

  private void attack(string type) {
    if (comboActive && attacks.Count < 7) {
      attacks.Add(type);

      foreach (var c in combos) {
        if (c.Matches(attacks)) {
          var obj = Resources.Load<GameObject>("Prefabs/" + type);

          var right = transform.position + new Vector3(2, 0);
          var left = transform.position + new Vector3(-2, 0);
          var up = transform.position + new Vector3(0, 2);
          var down = transform.position + new Vector3(0, -2);

          Instantiate(obj, up, Quaternion.identity);
          Instantiate(obj, down, Quaternion.identity);
          Instantiate(obj, left, Quaternion.identity);
          Instantiate(obj, right, Quaternion.identity);

          clearCombo();
          break;
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
    comboActive = Input.GetAxis("Combo") == 1;

    if (!comboActive) {
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
