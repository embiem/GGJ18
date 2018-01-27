using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCullingHandler : MonoBehaviour
{

  // Update is called once per frame
  void Update()
  {
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
    if (Physics.SphereCast(ray, 10f, out hit, 100f))
    {
      var rend = hit.collider.GetComponent<Renderer>();
      if (rend) {
      var prevColor = rend.material.GetColor("_Color");
      rend.material.SetColor("_Color", new Color(prevColor.r, prevColor.g, prevColor.b, 0.2f));
      }
    }
  }
}
