using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float speed = 8f;
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(0,0, 1 * speed, Space.Self);
	}
}
