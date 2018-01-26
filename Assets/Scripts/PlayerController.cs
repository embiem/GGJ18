using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_LOOK_DIRECTION
{
  LEFT = 1,
  BACK = 2,
  RIGHT = 3,
  FRONT = 4
}

public class PlayerController : MonoBehaviour
{
  [Header("Balancing Values")]
  public float speed = 6.0f;
  public float jumpSpeed = 8.0F;
  public float gravity = 20.0F;
  public float projectileSpeed = 10.0f;
	public float projectileSpawnOffset = 0.6f;
  public float animationSpeed = 5.0f;


  [Header("Refs")]
  public CharacterController CharacterController;
  public SpriteRenderer SpriteRenderer;
  public Sprite[] DirectionalSpritesLeft;
  public Sprite[] DirectionalSpritesRight;
  public Sprite[] DirectionalSpritesFront;
  public Sprite[] DirectionalSpritesBack;

  [Header("Prefabs")]
  public GameObject ProjectilePrefab;

  // privates
  private Vector3 moveDirection = Vector3.zero;
  private Vector3 lookDirection = new Vector3(0f, 0f, -1f);
  public float spriteAnimIdx = 0;

  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      var projectile = GameObject.Instantiate(
        ProjectilePrefab,
        transform.position + CharacterController.center + lookDirection * projectileSpawnOffset,
        Quaternion.LookRotation(lookDirection, Vector3.up)
      );
      projectile.GetComponent<Projectile>().Fire(lookDirection, projectileSpeed);
    }

    if (CharacterController.isGrounded)
    {
      moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
      this.CalculateLookDirection(moveDirection);

      moveDirection *= speed;
      if (Input.GetButton("Jump"))
        moveDirection.y = jumpSpeed;
    }
    moveDirection.y -= gravity * Time.deltaTime;
    CharacterController.Move(moveDirection * Time.deltaTime);

    // Sprite Animation
    var currentDirection = this.GetCurrentLookDirection();
    var currentSprites = DirectionalSpritesFront;
    switch (currentDirection)
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
    if (lookDirection.x != 0)
    {
      return lookDirection.x > 0 ? E_LOOK_DIRECTION.RIGHT : E_LOOK_DIRECTION.LEFT;
    }
    else if (lookDirection.z != 0)
    {
      return lookDirection.z > 0 ? E_LOOK_DIRECTION.BACK : E_LOOK_DIRECTION.FRONT;
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
