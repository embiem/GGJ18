using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCullingHandler : MonoBehaviour
{
  public LayerMask SphereCastLayers;
  public float TransparencyDistanceMod = 30f;
  private Dictionary<Renderer, Color> ChangedRenderers = new Dictionary<Renderer, Color>();

  // Update is called once per frame
  void Update()
  {
    Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height * 0.75f, 0f));
    RaycastHit[] hits = Physics.SphereCastAll(ray, 10f, 1000f, SphereCastLayers);
    if (hits.Length > 0)
    {
      for (var i = 0; i < hits.Length; i++)
      {
        var hit = hits[i];
        var rend = hit.collider.GetComponent<Renderer>();
        if (rend)
        {
          if (!ChangedRenderers.ContainsKey(rend))
          {
            var prevColor = rend.material.GetColor("_Color");
            ChangedRenderers.Add(rend, prevColor);
          }
          var distance = Vector3.Distance(hit.collider.transform.position, Camera.main.transform.position) * Mathf.Max(1.0f, hit.distance);
          rend.material.SetColor(
            "_Color",
            new Color(
              ChangedRenderers[rend].r,
              ChangedRenderers[rend].g,
              ChangedRenderers[rend].b,
              Mathf.Min(1.0f, distance / TransparencyDistanceMod))
            );
        }
      }
    }
  }
}
