using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public int health = 3;
    public Rigidbody projectile;
    public float reloadTime = 0.2f;

    bool hasTarget = false;
    GameObject currentTarget;

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.GetComponent<Enemy>())
        {
            if (!hasTarget) {
                currentTarget = other.gameObject;
                hasTarget = true;
                print("Target: " + other.name);
                AimAndFire(other.gameObject);
            }
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentTarget)
        {
            print("Enemy out of Range: " + other.name);
            hasTarget = false;
        }
    }

    void AimAndFire(GameObject enemy)
    {
        if (hasTarget)
        StartCoroutine("ShootAtTarget", enemy.transform.position);
    }

    IEnumerator ShootAtTarget(Vector3 enemyPos)
    {
        print("blubb");
        Instantiate(projectile, this.transform.position, Quaternion.identity);

   

        yield return new WaitForSeconds(reloadTime);

        if (hasTarget)
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
