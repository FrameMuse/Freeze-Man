using UnityEngine;

[AddComponentMenu("Character Move")]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class CharacterMove : MonoBehaviour
{
  private new Rigidbody rigidbody;
  // private Animator animator;
  float multiplier = 0.001f;

  void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
    // animator = GetComponent<Animator>();
  }

  void Update()
  {
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
    // transform.position += accelerationAxes * (multiplier + Time.deltaTime);
    rigidbody.AddForce(accelerationAxes * 50);
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
    //   animator.runtimeAnimatorController = Resources.Load("Assets/Kevin Iglesias/Giant Animations/AnimatorControllers/Giant@RunForward.controller") as RuntimeAnimatorController;
    //   return;
    // }

    // Walk
    // if (accelerationAxes.magnitude > 0)
    // {
    //   animator.runtimeAnimatorController = Resources.Load("Assets/Kevin Iglesias/Giant Animations/AnimatorControllers/Giant@WalkForward.controller") as RuntimeAnimatorController;
    //   return;
    // }

    // // Idle
    // if (accelerationAxes.magnitude == 0)
    // {
    //   animator.runtimeAnimatorController = Resources.Load("Assets/Kevin Iglesias/Giant Animations/AnimatorControllers/Giant@Idles.controller") as RuntimeAnimatorController;
    //   return;
    // }
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
