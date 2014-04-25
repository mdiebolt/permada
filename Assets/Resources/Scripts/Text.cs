using UnityEngine;
using System.Collections;

public class Text : MonoBehaviour {
  public string message;

  void OnGUI() {
    var position = new Rect(400, 250, 100, 50);
    var style = new GUIStyle();
    style.fontSize = 48;
    GUI.Label(position, message, style);
  }
}
