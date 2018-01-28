using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Installment
{
  [Header("Refs")]
  public GameObject projectileSpawnPoint;

  public int health = 20;

  public float reloadTime = 0.2f;
  public float projectileSpeed = 8f;
  public float projectileRange = 20f;

  public Projectile projectile;
  public GameObject laser;

  [Header("Rotation")]
  public GameObject parent;

  public float maxDegreesPerSecond = 30.0f;

  GameObject currentTarget;

  Quaternion qTo;

  private float lastShotTime = 0f;

  new void Start()
  {
    base.Start();
    StartCoroutine(CheckForTarget());
    laser.SetActive(false);
  }

  IEnumerator CheckForTarget()
  {
    yield return new WaitForSeconds(5f);
    DecideNextEnemy();
    StartCoroutine(CheckForTarget());
  }

  private void Update()
  {
    if (!currentTarget)
    {
      DecideNextEnemy();
    }

    if (currentTarget != null)
    {
      if (!laser.activeSelf)
      {
        laser.SetActive(true);
      }
      // Rotate
      Vector3 v3T = currentTarget.transform.position - parent.transform.position;
      v3T.y = parent.transform.position.y;
      qTo = Quaternion.LookRotation(v3T, Vector3.up);
      parent.transform.rotation = Quaternion.RotateTowards(parent.transform.rotation, qTo, maxDegreesPerSecond * Time.deltaTime);

      // Shoot
      if (Time.time - lastShotTime > reloadTime)
      {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up * -1, out hit, this.projectileRange))
        {
          if (hit.collider.gameObject == currentTarget)
          {
            Shoot();
            lastShotTime = Time.time;
          }
        }
      }
    }
    else
    {
      laser.SetActive(false);
    }
  }

  private void Shoot()
  {
    var spawnedProjectile = Instantiate(
      projectile,
      projectileSpawnPoint.transform.position,
      Quaternion.identity
    );

    spawnedProjectile.Fire(transform.up * -1, projectileSpeed);
  }

  private void DecideNextEnemy()
  {
    Enemy nearestEnemy = null;
    float nearestEnemyDistance = float.MaxValue;

    for (var i = 0; i < Enemy.ListOfEnemies.Count; i++)
    {
      var curEnemy = Enemy.ListOfEnemies[i];
      var distance = Vector3.Distance(transform.position, curEnemy.transform.position);
      if (distance < nearestEnemyDistance * Random.Range(0.85f, 1.15f)) // some randomness
      {
        nearestEnemyDistance = distance;
        nearestEnemy = curEnemy;
      }
    }
    if (nearestEnemy)
      this.currentTarget = nearestEnemy.gameObject;
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
