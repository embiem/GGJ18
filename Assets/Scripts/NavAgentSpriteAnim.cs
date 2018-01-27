using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentSpriteAnim : MonoBehaviour
{
  [Header("Balancing Values")]
  public float animationSpeed = 5.0f;

  [Header("Refs")]
  public NavMeshAgent NavMeshAgent;
  public SpriteRenderer SpriteRenderer;
  public Sprite[] DirectionalSpritesLeft;
  public Sprite[] DirectionalSpritesRight;
  public Sprite[] DirectionalSpritesFront;
  public Sprite[] DirectionalSpritesBack;

  // privates
  private Vector3 lookDirection = new Vector3(0f, 0f, -1f);
  private float lastLookDirCalc = 0;
  private float spriteAnimIdx = 0;

  public Vector3 LookDirection
  {
    get
    {
      return lookDirection;
    }

    set
    {
      lookDirection = value;
    }
  }

  void Update()
  {
    SpriteRenderer.transform.rotation = Quaternion.identity;

    var moveDirection = NavMeshAgent.velocity;

    if (Time.time - lastLookDirCalc > 1)
    {
      this.CalculateLookDirection(moveDirection);
      lastLookDirCalc = Time.time;
    }

    // Sprite Animation
    var currentLookDirection = this.GetCurrentLookDirection();
    var currentSprites = DirectionalSpritesFront;
    switch (currentLookDirection)
    {
      case E_LOOK_DIRECTION.BACK: currentSprites = DirectionalSpritesBack; break;
      case E_LOOK_DIRECTION.RIGHT: currentSprites = DirectionalSpritesRight; break;
      case E_LOOK_DIRECTION.LEFT: currentSprites = DirectionalSpritesLeft; break;
      default: currentSprites = DirectionalSpritesFront; break;
    }
    if (Mathf.Abs(moveDirection.x) > 0 || Mathf.Abs(moveDirection.z) > 0)
    {
      spriteAnimIdx += Time.deltaTime * this.animationSpeed;
    }
    else
    {
      spriteAnimIdx = 0;
    }
    SpriteRenderer.sprite = currentSprites[Mathf.FloorToInt(spriteAnimIdx) % currentSprites.Length];
  }


  E_LOOK_DIRECTION GetCurrentLookDirection()
  {
    if (LookDirection.x != 0)
    {
      return LookDirection.x > 0 ? E_LOOK_DIRECTION.RIGHT : E_LOOK_DIRECTION.LEFT;
    }
    else if (LookDirection.z != 0)
    {
      return LookDirection.z > 0 ? E_LOOK_DIRECTION.BACK : E_LOOK_DIRECTION.FRONT;
    }
    // fallback
    return E_LOOK_DIRECTION.FRONT;
  }

  void CalculateLookDirection(Vector3 moveDirection)
  {
    var absHorizontal = Mathf.Abs(moveDirection.x);
    var absVertical = Mathf.Abs(moveDirection.z);

    if (absHorizontal >= absVertical)
    {
      if (moveDirection.x > 0)
      {
        this.lookDirection.x = 1;
        this.lookDirection.z = 0;
      }
      else if (moveDirection.x < 0)
      {
        this.lookDirection.x = -1;
        this.lookDirection.z = 0;
      }
    }
    else
    {
      if (moveDirection.z > 0)
      {
        this.lookDirection.x = 0;
        this.lookDirection.z = 1;
      }
      else if (moveDirection.z < 0)
      {
        this.lookDirection.x = 0;
        this.lookDirection.z = -1;
      }
    }
  }
}
