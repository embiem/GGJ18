using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionTower : Installment
{
  [Header("Balancing Values")]
  public int health = 30;
	public float pointsPerSecond = 1f;

	[Header("Refs")]
  public GameObject BrokenTransmissionTowerPrefab;
	public Rotate RotateScript;
  public LineRenderer Laser;

	private float timeBucket = 0f;

	new void Start()
	{
		base.Start();
		RotateScript.enabled = false;
    Laser.SetPosition(0, Vector3.zero);
    Laser.SetPosition(1, Vector3.zero);
	}

  void Update()
  {
		if (GameManager.instance.RemainingSecondsToPrepare < 0) {
			if (!RotateScript.enabled) RotateScript.enabled = true;

      Laser.SetPosition(1, Vector3.forward * Mathf.Min(10, GameManager.instance.SecondsSinceStartOfTransmission));

			timeBucket += Time.deltaTime;

			if (timeBucket > 1f) {
				timeBucket -= 1f;
				GameManager.instance.TransmissionPoints += this.pointsPerSecond;
			}
		}
  }

  public void TakeDamage(int damage = 1)
  {
    this.health -= damage;
    if (this.health <= 0)
    {
      this.OnDeath();
    }
  }


  private void OnDeath()
  {
    Instantiate(BrokenTransmissionTowerPrefab, transform.position, transform.rotation);
    Destroy(this.gameObject);
  }
}
