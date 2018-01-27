using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public int health = 3;

    public float reloadTime = 0.2f;

    public Rigidbody projectile;

    [Header("Rotation")]
    public GameObject parent;

    public float maxDegreesPerSecond = 30.0f;

    GameObject target;

    GameObject currentTarget;

    Quaternion qTo;

    bool hasTarget = false;


    private void OnTriggerEnter(Collider other)
    {
     
        if (other.GetComponent<Enemy>())
        {
            if (!hasTarget) {
                currentTarget = other.gameObject;
                hasTarget = true;
                AimAndFire(other.gameObject);
            }
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentTarget)
        {
            hasTarget = false;
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
        if (hasTarget)
        StartCoroutine("ShootAtTarget", enemy.transform.position);
    }

    IEnumerator ShootAtTarget(Vector3 enemyPos)
    {
 
        Instantiate(projectile, this.transform.position, Quaternion.identity);

        projectile.AddForce(transform.forward * 200 * Time.deltaTime);

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
