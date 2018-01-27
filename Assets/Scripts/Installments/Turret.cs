using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
  [Header("Refs")]
  public GameObject projectileSpawnPoint;

  public int health = 3;

  public float reloadTime = 0.2f;
  public float projectileSpeed = 8f;

  public Projectile projectile;

  [Header("Rotation")]
  public GameObject parent;

  public float maxDegreesPerSecond = 30.0f;

  GameObject target;

  GameObject currentTarget;

  Quaternion qTo;

  private void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<Enemy>())
    {
      if (!currentTarget)
      {
        currentTarget = other.gameObject;
        AimAndFire(other.gameObject);
      }

    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject == currentTarget)
    {
      currentTarget = null;
    }
  }

  private void Update()
  {
    if (currentTarget != null)
    {
      Vector3 v3T = currentTarget.transform.position - parent.transform.position;
      v3T.y = parent.transform.position.y;
      qTo = Quaternion.LookRotation(v3T, Vector3.up);
      parent.transform.rotation = Quaternion.RotateTowards(parent.transform.rotation, qTo, maxDegreesPerSecond * Time.deltaTime);
    }

  }

  void AimAndFire(GameObject enemy)
  {
    if (currentTarget)
      StartCoroutine("ShootAtTarget", enemy.transform.position);
  }

  IEnumerator ShootAtTarget(Vector3 enemyPos)
  {

    var spawnedProjectile = Instantiate(
        projectile,
        projectileSpawnPoint.transform.position,
        Quaternion.identity
    );

    spawnedProjectile.Fire(transform.forward, projectileSpeed);

    yield return new WaitForSeconds(reloadTime);

    if (currentTarget)
    {
      AimAndFire(currentTarget);
    }
  }

  public void TakeDamage(int damage)
  {
    this.health -= damage;
    if (this.health <= 0)
    {
      this.OnDeath();
    }
  }


  private void OnDeath()
  {
    Destroy(this.gameObject);
  }
}
