using UnityEngine;
using System.Collections;

public class Expirable : MonoBehaviour {
  [HideInInspector]
  public float age = 0;

  public float duration = 2f;

	void Update() {
    age += Time.deltaTime;

    if (age > duration)
      Destroy(gameObject);	
	}
}
