using UnityEngine;

[AddComponentMenu("Character/Character Movement")]
[RequireComponent(typeof(CharacterController))]
[DisallowMultipleComponent]
public class CharacterMovement : MonoBehaviour
{
  CharacterController characterController;
  [SerializeField]
  Vector3 velocity;
  [SerializeField]
  bool sloped;
  [SerializeField]
  float slopeDistance = 0.03f;
  [SerializeField]
  bool grounded;
  public float runSpeed = 400f;
  public float walkSpeed = 200f;
  float jumpHeight = 1.0f;
  float gravityValue = -9.81f;

  FollowingCamera followingCamera;

  void Start()
  {
    followingCamera = FindObjectOfType<FollowingCamera>();
    characterController = GetComponent<CharacterController>();
  }

  void Update()
  {
    float playerSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;


    sloped = !Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hitInfo, slopeDistance);

    grounded = characterController.isGrounded;
    if (grounded && velocity.y < 0)
    {
      velocity.y = 0f;
    }

    float xAxis = Input.GetAxis("Horizontal");
    float yAxis = Input.GetAxis("Vertical");

    Vector3 moveDirection = new(xAxis, 0, yAxis);
    Vector3 lookDirection = followingCamera.GetLookDirection();

    // float xAcceleration = xAxis * playerSpeed;
    // float yAcceleration = yAxis * playerSpeed;

    // Vector3 moveAxis = new(xAcceleration, 0, yAcceleration);
    // characterController.SimpleMove(moveAxis * Time.deltaTime);

    if (lookDirection != Vector3.zero)
    {
      transform.forward = new(lookDirection.x, transform.forward.y, lookDirection.z);
    }

    characterController.SimpleMove(transform.TransformDirection(moveDirection) * playerSpeed * Time.deltaTime);

    if (moveDirection != Vector3.zero)
    {
      transform.forward = transform.TransformDirection(moveDirection);
    }




    // transform.Rotate(moveDirection);

    // Changes the height position of the player
    if (Input.GetButtonDown("Jump") && !sloped && grounded)
    {
      velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

    velocity.y += gravityValue * Time.deltaTime;
    characterController.Move(velocity * Time.deltaTime);
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.magenta;
    Gizmos.DrawRay(transform.position, Vector3.down * slopeDistance);
  }
}
