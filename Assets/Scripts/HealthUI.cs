using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
  public Sprite[] HealthStepSprites;

  private PlayerController player;
  private Image image;
  private int curHealthIdx;

  void Start()
  {
		this.image = GetComponent<Image>();
    this.player = FindObjectOfType<PlayerController>();
    this.curHealthIdx = HealthStepSprites.Length - 1;
    this.image.sprite = HealthStepSprites[curHealthIdx];
  }

  void Update()
  {
    var newIdx = Mathf.FloorToInt(1.0f * player.Health / player.StartingHealth * (HealthStepSprites.Length - 1));
    if (newIdx != curHealthIdx)
    {
      curHealthIdx = newIdx;
      this.image.sprite = HealthStepSprites[Mathf.Clamp(curHealthIdx, 0, HealthStepSprites.Length)];
    }
  }
}
