using UnityEngine;
using System.Collections;

namespace Player {
  public class Attack : MonoBehaviour {
    private bool swinging = false;
    private float swingFor = 0; // How many degrees to swing across
    private float swingFrom = 0; // Where to start swinging
    private int swungDegrees = 0;

    private IEnumerator CountTo(float duration) {
      float elapsed = 0;
      
      while (elapsed < duration) {
        elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
        yield return null;
      }
    }

    private IEnumerator ghost() {
      var collider = gameObject.GetComponent<CircleCollider2D>();
      collider.enabled = false;

      var spriteRenderer = GetComponent<SpriteRenderer>();
      var color = spriteRenderer.color;
      color.a = 0.3f;

      spriteRenderer.color = color;

      yield return StartCoroutine(CountTo(10));

      collider.enabled = true;

      color.a = 1.0f;
      spriteRenderer.color = color;
    }

    private IEnumerator bigHitPause() {
      int elapsed = 0;
      int duration = 30;

      Time.timeScale = 0;

      while (elapsed < duration) {
        elapsed += 1; 
        yield return null;
      }

      Time.timeScale = 1.0f;
    }

    private void SetWeaponEnabled(bool value) {
      var weapon = GameObject.FindGameObjectWithTag("Weapon");

      weapon.renderer.enabled = value;
      weapon.GetComponent<BoxCollider2D>().enabled = value;
    }

    private void FlipWeapon(Vector3 velocity) {
      var arrow = transform.Find("Weapon");

      if (velocity.y > 0 && !swinging)
        arrow.rotation = Quaternion.Euler(0, 0, 90);
      else if (velocity.y < 0 && !swinging)
        arrow.rotation = Quaternion.Euler(0, 0, 270);

      if (velocity.x > 0 && !swinging)
        arrow.rotation = Quaternion.Euler(0, 0, 0);
      else if (velocity.x < 0 && !swinging)
        arrow.rotation = Quaternion.Euler(0, 0, 180);
    }

    void Swing() {
      var arrow = transform.Find("Weapon");
      var rot = arrow.eulerAngles;

      if (swungDegrees < swingFor) {
        swungDegrees += 5;
        rot.z += 5;
      } else {
        swinging = false;
        swungDegrees = 0;

        SetWeaponEnabled(false);
      }

      arrow.eulerAngles = rot;
    }

    void FixedUpdate() {

    }

  	void Update() {
      var facing = GetComponent<Move>().facing;

      FlipWeapon(facing);

      if (Input.GetKeyDown("b")) {
        var bomb = Resources.Load<GameObject>("Prefabs/Bomb");
        Instantiate(bomb, transform.position, Quaternion.identity);
      }

      if (Input.GetKeyDown("a")) {
        var prefab = Resources.Load<GameObject>("Prefabs/Arrow");
        Instantiate(prefab, transform.position, Quaternion.identity);
      }

      if (Input.GetKeyDown("g")) {
        var grave = GameObject.Find("Grave");
        var distance = Vector3.Distance(grave.transform.position, transform.position);

        if (distance < 1.25f) {
          StartCoroutine(ghost());
        }
      }

      if (Input.GetKeyDown("o")) {
        var camera = GameObject.FindWithTag("MainCamera");
        StartCoroutine(camera.transform.Shake(0.25f, 0.25f));
      }

      if (Input.GetKeyDown("t")) {
        StartCoroutine(bigHitPause());
      }

      if (Input.GetKeyDown("space") && !swinging) {
        var arrow = transform.Find("Weapon");
        var rot = arrow.eulerAngles;

        swingFor = 90;
        swinging = true;

        SetWeaponEnabled(true);

        if (facing.x > 0)
          swingFrom = -45.0f;
        else if (facing.x < 0)
          swingFrom = 135.0f;
        else if (facing.y > 0)
          swingFrom = 45.0f;
        else if (facing.y < 0)
          swingFrom = 235.0f;

        rot.z = swingFrom;
        arrow.eulerAngles = rot;
      }

      if (swinging)
        Swing();
  	}
  }
}