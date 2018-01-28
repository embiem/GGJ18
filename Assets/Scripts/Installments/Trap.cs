using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Installment
{
  public Animator explosion;
  public AudioClip AudioExplosion;

  public GameObject thisObject;

  Renderer rend;

  Collider col;



  new void Start()
  {
    base.Start();
    rend = this.gameObject.GetComponent<Renderer>();
    col = this.gameObject.GetComponent<Collider>();
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<Enemy>())
    {
      var e = other.GetComponent<Enemy>();
      e.TakeDamage(3);

      this.StartCoroutine("DeactivateTrap");
    }
  }

  IEnumerator DeactivateTrap()
  {

    var shaker = FindObjectOfType<ScreenShake>();

    if (shaker != null)
    {
      shaker.StartScreenShake();
    }

    this.explosion.Play("Expl");
    AudioSource.PlayClipAtPoint(AudioExplosion, transform.position);

    if (col != null)
      col.enabled = false;

    if (rend != null)
      rend.enabled = false;
    yield return new WaitForSeconds(0.5f);
    Kill();

  }

  void Kill()
  {
    DestroyImmediate(thisObject);
  }
}
