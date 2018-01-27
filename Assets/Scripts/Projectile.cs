using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  [Header("Refs")]
  public CharacterController CharacterController;
  public ParticleSystem ParticleSystem;
	public GameObject Model;

  private Vector3 direction;
  private float speed;
  private bool isFired;

  // Use this for initialization
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    if (isFired && CharacterController.enabled)
    {
			if (Mathf.Abs(transform.position.x) > 100 || Mathf.Abs(transform.position.y) > 100 || Mathf.Abs(transform.position.z) > 100) {
				Destroy(this.gameObject);
				return;
			}
			 CharacterController.Move(this.speed * this.direction * Time.deltaTime);
    }
  }

  void OnControllerColliderHit(ControllerColliderHit hit)
  {
    if (isFired)
    {
			ParticleSystem.Play();

			Destroy(this.gameObject, 1f);
			Model.SetActive(false);
			CharacterController.enabled = false;
			this.isFired = false;

			var enemy = hit.gameObject.GetComponent<Enemy>();
			if (enemy) {
				enemy.TakeDamage();
			}
    }
  }

  public void Fire(Vector3 direction, float speed)
  {
    this.direction = direction;
    this.speed = speed;
    this.isFired = true;
  }
}
