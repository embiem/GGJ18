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
  public float projectileCooldown = 0.5f;
  public float projectileSpawnOffset = 0.6f;
  public float meleeWeaponSpeed = 10.0f;
  public float meleeRange = 2f;
  public float meleeCooldown = 1f;
  public float animationSpeed = 5.0f;
  public float pushPower = 2.0F;
  public int damage = 1;


  [Header("Refs")]
  public CharacterController CharacterController;
  public SpriteRenderer SpriteRenderer;
  public Sprite[] DirectionalSpritesLeft;
  public Sprite[] DirectionalSpritesRight;
  public Sprite[] DirectionalSpritesFront;
  public Sprite[] DirectionalSpritesBack;
  public Animator MeleeWeapon;

  [Header("Prefabs")]
  public GameObject ProjectilePrefab;

  // privates
  private Vector3 moveDirection = Vector3.zero;
  private Vector3 lookDirection = new Vector3(0f, 0f, -1f);
  private float spriteAnimIdx = 0;
  private float lastMeleeTime = 0.0f;
  private float lastProjectileTime = 0.0f;

  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonDown("Fire1") && Time.time - this.lastProjectileTime > projectileCooldown)
    {
      var projectile = GameObject.Instantiate(
        ProjectilePrefab,
        transform.position + CharacterController.center + lookDirection * projectileSpawnOffset,
        Quaternion.LookRotation(lookDirection, Vector3.up)
      );
      projectile.GetComponent<Projectile>().Fire(lookDirection, projectileSpeed);
      this.lastProjectileTime = Time.time;
    }

    if (Input.GetButton("Fire2") && Time.time - this.lastMeleeTime > meleeCooldown)
    {
      this.Melee();
      this.lastMeleeTime = Time.time;
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

  void OnControllerColliderHit(ControllerColliderHit hit)
  {
    Rigidbody body = hit.collider.attachedRigidbody;
    if (body == null || body.isKinematic)
      return;

    if (hit.moveDirection.y < -0.3F)
      return;

    Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
    body.velocity = pushDir * pushPower;
  }

  void Melee()
  {
    switch (this.GetCurrentLookDirection())
    {
      case E_LOOK_DIRECTION.BACK: MeleeWeapon.SetTrigger("back"); break;
      case E_LOOK_DIRECTION.RIGHT: MeleeWeapon.SetTrigger("right"); break;
      case E_LOOK_DIRECTION.LEFT: MeleeWeapon.SetTrigger("left"); break;
      default: MeleeWeapon.SetTrigger("front"); break;
    }

    // check for enemy-hit
    RaycastHit hit;
    Debug.DrawRay(transform.position, this.lookDirection * this.meleeRange, Color.green, 3, false);
    if (Physics.Raycast(transform.position, this.lookDirection, out hit, this.meleeRange))
    {
      var enemy = hit.collider.GetComponent<Enemy>();
      if (enemy)
      {
        enemy.TakeDamage(damage);
      }
    }
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
