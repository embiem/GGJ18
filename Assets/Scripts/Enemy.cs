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

  // Use this for initialization
  void Start()
  {
    this.agent = GetComponent<NavMeshAgent>();
    this.player = FindObjectOfType<PlayerController>();
  }

  // Update is called once per frame
  void Update()
  {
		this.agent.destination = this.player.transform.position;
  }

  public void TakeDamage(int damage)
  {
    this.Health -= damage;
    if (this.Health <= 0)
    {
      this.OnDeath();
    }
  }


  private void OnDeath()
  {
    Destroy(this.gameObject);
  }
}
