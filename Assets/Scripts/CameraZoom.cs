using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraZoom : MonoBehaviour {

    public Camera cam;

    public Vector3 camStartPosition;
    public Vector3 camDestination = new Vector3(0f, 3.45f, -5.3f);
    public float zoomSpeed = 1f;
    public float delayTillZoom = 2f;


	// Use this for initialization
	void Start () {
  
        cam.transform.position = camStartPosition;
        StartCoroutine("WaitForZoom");
	}

    IEnumerator WaitForZoom()
    {
        yield return new WaitForSeconds(delayTillZoom);
        ZoomOut();
    }

    void ZoomOut()
    {
        cam.transform.DOLocalMove(camDestination, zoomSpeed, false);
    }
}
