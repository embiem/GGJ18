using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
  [Header("Balancing Values")]
  public int Health = 10;

  NavMeshAgent agent;
  PlayerController player;
  Installment installmentTarget;

  // Use this for initialization
  void Start()
  {
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

    if (this.installmentTarget)
    {
      this.agent.destination = this.installmentTarget.transform.position;
    }
    else
    {
      this.agent.destination = this.player.transform.position;
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
      var distance = Vector3.Distance(transform.position, curInstallment.transform.position);
      if (distance < nearestInstallmentDistance)
      {
        nearestInstallmentDistance = distance;
        nearestInstallment = curInstallment;
      }
    }

    this.installmentTarget = nearestInstallment;
  }


  private void OnDeath()
  {
    Destroy(this.gameObject);
  }
}
