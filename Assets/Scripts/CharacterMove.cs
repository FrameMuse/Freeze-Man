using UnityEngine;

[AddComponentMenu("Character Move")]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class CharacterMove : MonoBehaviour
{
  public float maxVelocity = 2;
  public RuntimeAnimatorController IdleAnimationController;
  public RuntimeAnimatorController WalkAnimationController;
  public RuntimeAnimatorController RunAnimationController;
  private new Rigidbody rigidbody;
  private Animator animator;
  float multiplier = 0.001f;
  public bool Awake;

  void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    rigidbody.WakeUp();
    Awake = !rigidbody.IsSleeping();
    // Address move events
    onAxesUpdate();
  }
  // private Vector3 accelerationAxes;
  void onAxesUpdate()
  {
    Vector3 accelerationAxes = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    move(accelerationAxes);
    face(accelerationAxes);
    animate(accelerationAxes);
  }

  /// <summary>
  /// Translate character to a certain values
  /// </summary>
  void move(Vector3 accelerationAxes)
  {
    if (rigidbody.velocity.magnitude >= maxVelocity) return;
    rigidbody.AddForce(Vector3.Normalize(accelerationAxes) * 15);
  }

  /// <summary>
  /// Rotate character's body to a certain direction
  /// </summary>
  void face(Vector3 accelerationAxes)
  {
    if (accelerationAxes == Vector3.zero) return;

    transform.rotation = Quaternion.Slerp(
        transform.rotation,
        Quaternion.LookRotation(accelerationAxes),
        Time.deltaTime * 5f
    );

    // transform.LookAt(transform.position + accelerationAxes);
  }

  void animate(Vector3 accelerationAxes)
  {
    // // Run
    // if (accelerationAxes.magnitude > 0.5)
    // {
    //   animator.runtimeAnimatorController = RunAnimationController;
    //   return;
    // }

    // Walk
    if (accelerationAxes.magnitude > 0)
    {
      animator.runtimeAnimatorController = WalkAnimationController;
      return;
    }

    // Idle
    if (accelerationAxes.magnitude == 0)
    {
      animator.runtimeAnimatorController = IdleAnimationController;
      return;
    }
  }
  // private void OnDrawGizmos()
  // {
  //   Gizmos.color = Color.red;
  //   Gizmos.DrawRay(new Ray(transform.position + new Vector3(0, 0.5f), new Vector3(1, 0, 0)));
  //   Gizmos.DrawRay(new Ray(transform.position + new Vector3(0, 0.5f), new Vector3(0, 0, 1)));

  //   Gizmos.color = Color.blue;
  //   Gizmos.DrawRay(new Ray(transform.position + new Vector3(0, 0.5f), accelerationAxes));
  // }

  void OnMouseDown()
  {
    Quaternion rotation = transform.rotation;
    Vector3 eulerAngles = rotation.eulerAngles;
    eulerAngles.x = 0;
    eulerAngles.y = 0;
    eulerAngles.z = 0;
    rotation.eulerAngles = eulerAngles;
    transform.rotation = rotation;
  }
}
