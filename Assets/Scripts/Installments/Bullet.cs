using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float lifeTime = 5f;
    Rigidbody rb;
    float start = 0;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}


    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward * 200 * Time.deltaTime);
    }
}
