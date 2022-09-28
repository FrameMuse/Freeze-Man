using UnityEngine;

[AddComponentMenu("Character/Character Movement")]
[RequireComponent(typeof(CharacterController))]
[DisallowMultipleComponent]
public class CharacterMovement : MonoBehaviour
{
  CharacterController characterController;
  FollowingCamera followingCamera;


  public float runSpeed = 4f;
  public float walkSpeed = 2f;
  public float jumpHeight = 1.0f;

  [Header("Gravity")]
  [SerializeField]
  Vector3 velocity;
  [SerializeField]
  float gravityValue = -9.81f;
  public float mass = 1f;
  [SerializeField]
  bool airborne;
  [SerializeField]
  Vector3 slopeSpherePosition;
  [SerializeField]
  float slopeSphereRadius = 0.1f;


  void Start()
  {
    followingCamera = FindObjectOfType<FollowingCamera>();
    characterController = GetComponent<CharacterController>();
  }

  void Update()
  {
    MoveAndRotate();
    HandleJumping();
    ApplyGravity();
  }

  float playerSpeed
  {
    get
    {
      float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

      if (airborne) return speed * 0.1f;
      return speed;
    }
  }

  void MoveAndRotate()
  {
    float xAxis = Input.GetAxis("Horizontal");
    float yAxis = Input.GetAxis("Vertical");

    Vector3 moveDirection = new(xAxis, 0, yAxis);
    Vector3 lookDirection = followingCamera.GetLookDirection();

    if (moveDirection != Vector3.zero)
    {

      moveDirection = followingCamera.transform.rotation * moveDirection;
      moveDirection.Normalize();
      moveDirection.y = 0;
      // if (lookDirection != Vector3.zero)
      //   transform.forward = followingCamera.transform.rotation * new Vector3(lookDirection.x, transform.forward.y, lookDirection.z);

      // Vector3 transformedMoveDirection = transform.TransformDirection(moveDirection);

      transform.forward = moveDirection;
      characterController.Move(moveDirection * playerSpeed * Time.deltaTime);
    }
  }

  void HandleJumping()
  {
    if (Input.GetButtonDown("Jump") && !airborne)
      velocity.y += jumpHeight;
  }

  void ApplyGravity()
  {
    airborne = !Physics.CheckSphere(transform.position + slopeSpherePosition, slopeSphereRadius);
    if ((characterController.isGrounded || !airborne) && velocity.y < 0)
    {
      ApplyGravityImpact(velocity);
      velocity.y = 0f;
    }

    velocity.y += gravityValue * Time.deltaTime;
    characterController.Move(velocity * Time.deltaTime);
  }

  void ApplyGravityImpact(Vector3 velocity) { }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.magenta;
    Gizmos.DrawSphere(transform.position + slopeSpherePosition, slopeSphereRadius);
  }
}
