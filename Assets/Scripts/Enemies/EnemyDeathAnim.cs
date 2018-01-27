using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathAnim : MonoBehaviour
{
  public float animationSpeed = 5.0f;
  public Sprite[] DeathAnimSprites;
  private float spriteAnimIdx = 0;

  private SpriteRenderer SpriteRenderer;

  // Use this for initialization
  void Start()
  {
    this.SpriteRenderer = GetComponent<SpriteRenderer>();
    transform.rotation = Quaternion.identity;
  }

  // Update is called once per frame
  void Update()
  {
    spriteAnimIdx += Time.deltaTime * this.animationSpeed;

    if (Mathf.FloorToInt(spriteAnimIdx) >= DeathAnimSprites.Length)
    {
      Destroy(this.gameObject);
    }
    else
    {
      SpriteRenderer.sprite = DeathAnimSprites[Mathf.FloorToInt(spriteAnimIdx) % DeathAnimSprites.Length];
    }
  }
}
