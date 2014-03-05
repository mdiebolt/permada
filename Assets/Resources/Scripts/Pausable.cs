using UnityEngine;
using System.Collections;

public class Pausable : MonoBehaviour {
  public IEnumerator HitPause() {
    int elapsed = 0;
    int duration = 30;
    
    Time.timeScale = 0;
    
    while (elapsed < duration) {
      elapsed += 1; 
      yield return null;
    }
    
    Time.timeScale = 1.0f;
  }
}
