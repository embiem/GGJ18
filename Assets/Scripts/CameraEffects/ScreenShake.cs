using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenShake : MonoBehaviour
{
    public Camera cam;
    public float duration = 0.5f;
    public float strength = 1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartScreenShake();
        }
    }

    public void StartScreenShake()
    {
        cam.transform.DOShakePosition(duration, strength);    
    }

}
