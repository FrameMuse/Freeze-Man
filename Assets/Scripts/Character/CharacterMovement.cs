using UnityEngine;

[AddComponentMenu("Character/Character Movement")]
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
  CharacterController characterController;
  [SerializeField]
  Vector3 velocity;
  [SerializeField]
  bool sloped;
  [SerializeField]
  bool grounded;
  float playerSpeed = 2.0f;
  float jumpHeight = 1.0f;
  float gravityValue = -9.81f;

  void Start()
  {
    characterController = GetComponent<CharacterController>();
  }

  float maxRaycastDistance = 0.03f;

  void Update()
  {
    RaycastHit hitInfo;
    sloped = !Physics.Raycast(new Ray(transform.position, Vector3.down), out hitInfo, maxRaycastDistance);

    grounded = characterController.isGrounded;
    if (grounded && velocity.y < 0)
    {
      velocity.y = 0f;
    }

    Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    characterController.Move(move * Time.deltaTime * playerSpeed);

    if (move != Vector3.zero)
    {
      gameObject.transform.forward = move;
    }

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
    Gizmos.DrawRay(transform.position, Vector3.down * maxRaycastDistance);
  }
}
