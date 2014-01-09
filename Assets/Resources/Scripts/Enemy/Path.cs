using UnityEngine;
using System.Collections;

namespace Enemy {
  public class Path : MonoBehaviour {
    public Vector3[] path;

    public float moveTime;
    public float pauseTime;

    public EaseType moveEase;

    private Vector3 startPosition;

    void Start() {
      startPosition = transform.position;
      StartCoroutine(MoveOnPath());
    }
    
    IEnumerator MoveOnPath() {
      // Snap to the first position
      transform.localPosition = path[0] + startPosition;
      int pathIndex = 1;
      
      while (true) {
        yield return StartCoroutine(transform.MoveTo(path[pathIndex] + startPosition, moveTime, moveEase));
        yield return StartCoroutine(Auto.Wait(pauseTime));
        
        // Find the next point
        pathIndex++;
        if (pathIndex == path.Length)
          pathIndex = 0;
      }
    }
  }
}
