using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
  public static List<Enemy> ListOfEnemies = new List<Enemy>();

  [Header("Balancing Values")]
  public bool isMeleeArchetype = true;
  public float projectileRange = 10f;
  public float projectileSpeed = 10.0f;
  public float attackCooldown = 1f;
  public int Health = 10;

  [Header("Refs")]
  public Projectile ProjectilePrefab;
  public GameObject DeathAnimPrefab;

  NavMeshAgent agent;
  PlayerController player;
  Installment installmentTarget;
  float lastAttackTime = 0f;

  public GameObject CurrentTarget
  {
    get
    {
      return installmentTarget ? installmentTarget.gameObject : player.gameObject;
    }
  }

  // Use this for initialization
  void Start()
  {
    ListOfEnemies.Add(this);

    this.agent = GetComponent<NavMeshAgent>();
    this.player = FindObjectOfType<PlayerController>();
    StartCoroutine(CheckForTarget());
  }

  IEnumerator CheckForTarget()
  {
    yield return new WaitForSeconds(2f);
    DecideNextInstallment();
    StartCoroutine(CheckForTarget());
  }

  // Update is called once per frame
  void Update()
  {
    if (!this.installmentTarget)
    {
      DecideNextInstallment();
    }

    if (this.IsInRange())
    {
      this.agent.destination = transform.position;

      if (Time.time - lastAttackTime > attackCooldown)
      {
        lastAttackTime = Time.time;
        this.Attack();
      }
    }
    else
    {
      if (this.installmentTarget)
      {
        this.agent.destination = this.installmentTarget.transform.position;
      }
      else
      {
        this.agent.destination = this.player.transform.position;
      }
    }
  }

  public void TakeDamage(int damage)
  {
    this.Health -= damage;
    if (this.Health <= 0)
    {
      this.OnDeath();
    }
  }

  private void DecideNextInstallment()
  {
    Installment nearestInstallment = null;
    float nearestInstallmentDistance = float.MaxValue;

    for (var i = 0; i < Installment.ListOfInstallments.Count; i++)
    {
      var curInstallment = Installment.ListOfInstallments[i];
      if (curInstallment.EnemyShouldIgnore) continue;

      var distance = Vector3.Distance(transform.position, curInstallment.transform.position);
      if (distance < nearestInstallmentDistance * Random.Range(0.85f, 1.15f)) // some randomness
      {
        nearestInstallmentDistance = distance;
        nearestInstallment = curInstallment;
      }
    }

    var playerDistance = Vector3.Distance(transform.position, this.player.transform.position);
    this.installmentTarget = playerDistance * 2f < nearestInstallmentDistance ? null : nearestInstallment;
  }

  private bool IsInRange()
  {
    var distance = Vector3.Distance(CurrentTarget.transform.position, transform.position);

    if (distance < agent.stoppingDistance + 0.5f)
    {
      return true;
    }

    if (!isMeleeArchetype)
    {
      RaycastHit hit;
      if (Physics.Raycast(transform.position, transform.forward, out hit, this.projectileRange))
      {
        return hit.collider.gameObject == CurrentTarget;
      }
    }

    return false;
  }

  private void Attack()
  {
    if (isMeleeArchetype)
    {
      Debug.Log("Melee Attack");
    }
    else
    {
      var projectile = GameObject.Instantiate(
        ProjectilePrefab,
        transform.position + Vector3.up * 0.5f + transform.forward,
        Quaternion.LookRotation(transform.forward, Vector3.up)
      );
      projectile.GetComponent<Projectile>().Fire(transform.forward, projectileSpeed);
    }
  }

  private void OnDeath()
  {
    Instantiate(DeathAnimPrefab, transform.position, Quaternion.identity);
    Destroy(this.gameObject);
  }

  void OnDestroy()
  {
    ListOfEnemies.Remove(this);
  }
}
