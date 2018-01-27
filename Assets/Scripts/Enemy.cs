using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[Header("Balancing Values")]
	public int Health = 10;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

	public void TakeDamage() {
		this.Health -= 1;
		if (this.Health <= 0) {
			this.OnDeath();
		}
	}


	private void OnDeath()
	{
		Destroy(this.gameObject);
	}
}
