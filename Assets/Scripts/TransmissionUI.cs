using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransmissionUI : MonoBehaviour
{
  public RectTransform MaskController;
  public Text StatusText;
  public Text PercentageText;
  public Vector2 MaskMinMax;

  private bool flippedStatusText = false;

  void Start()
  {
    StatusText.text = "Transmission Starting in...";
  }

  // Update is called once per frame
  void Update()
  {
    if (GameManager.instance.RemainingSecondsToPrepare > 0)
    {
      var lerpedVal = Mathf.Lerp(MaskMinMax.x, MaskMinMax.y, Mathf.Abs(Mathf.Sin(Time.time)));
      MaskController.sizeDelta = new Vector2(lerpedVal, MaskController.sizeDelta.y);
      PercentageText.text = (int)(GameManager.instance.RemainingSecondsToPrepare) + " seconds";
    }
    else
    {
      if (!flippedStatusText)
      {
        flippedStatusText = true;
        StatusText.text = "Transmission Progress";
      }
      var lerpedVal = Mathf.Lerp(MaskMinMax.x, MaskMinMax.y, GameManager.instance.TransmissionProgress);
      MaskController.sizeDelta = new Vector2(lerpedVal, MaskController.sizeDelta.y);
      PercentageText.text = (int)(GameManager.instance.TransmissionProgress * 100) + "%";
    }
  }
}
