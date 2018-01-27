using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public ParticleSystem explosionPart;

    Renderer rend;

    Collider col;

    private void Start()
    {
        explosionPart.Stop();
        explosionPart.Clear();
       rend = this.gameObject.GetComponent<Renderer>();
        col = this.gameObject.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            print("is Enemy!");
            var e = other.GetComponent<Enemy>();
            e.TakeDamage(3);

            this.DeactivateTrap();
        }
    }

    void DeactivateTrap()
    {
        this.explosionPart.Play();

        if(col != null)
        col.enabled = false;

        if(rend != null)
        rend.enabled = false;


    }

}
