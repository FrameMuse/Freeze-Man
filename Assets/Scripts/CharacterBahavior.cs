using System;
using UnityEngine;

[AddComponentMenu("Character/Character Bahavior")]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class CharacterBahavior : MonoBehaviour
{

  public CharacterTransformSettings transformSettings;
  public CharacterAnimationSettings animationSettings;
  private new Rigidbody rigidbody;
  private Animator animator;

  void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    // Prevent rigidbody from sleeping
    if (rigidbody.IsSleeping()) rigidbody.WakeUp();
    // Address axes events
    axesUpdate();

    animator.speed = animationSettings.speed;
  }
  // private Vector3 accelerationAxes;
  void axesUpdate()
  {
    Vector3 inputAccelerationAxes = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    // inputAccelerationAxes.Normalize();

    move(inputAccelerationAxes);
    face(inputAccelerationAxes);
    animate(inputAccelerationAxes);
  }

  /// <summary>
  /// Translate character to a certain values
  /// </summary>
  void move(Vector3 accelerationAxes)
  {
    if (rigidbody.velocity.magnitude >= transformSettings.maxForwardVelocity) return;
    rigidbody.AddForce(accelerationAxes * transformSettings.forwardAccelerationMultiplier);
  }

  /// <summary>
  /// Rotate character's body to a certain direction
  /// <see href="https://answers.unity.com/questions/46845/face-forward-direction-of-movement.html">Reference</see>
  /// </summary>
  void face(Vector3 accelerationAxes)
  {
    if (accelerationAxes == Vector3.zero) return;

    transform.rotation = Quaternion.Slerp(
        transform.rotation,
        Quaternion.LookRotation(accelerationAxes),
        Time.deltaTime * transformSettings.rotateAccelerationMultiplier
    );

    // transform.LookAt(transform.position + accelerationAxes);
  }

  void animate(Vector3 accelerationAxes)
  {
    // Walk
    if (accelerationAxes.magnitude > 0)
    {
      animator.runtimeAnimatorController = animationSettings.walkController;
      return;
    }

    // Idle
    animator.runtimeAnimatorController = animationSettings.idleController;
  }
  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawRay(new Ray(transform.position + new Vector3(0, 0.5f), new Vector3(1, 0, 0)));
    Gizmos.DrawRay(new Ray(transform.position + new Vector3(0, 0.5f), new Vector3(0, 0, 1)));

    Gizmos.color = Color.blue;
    Vector3 inputAccelerationAxes = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    Gizmos.DrawRay(new Ray(transform.position + new Vector3(0, 0.5f), inputAccelerationAxes));
  }

  private void OnMouseDown()
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

[Serializable]
public class CharacterTransformSettings
{
  public float forwardAccelerationMultiplier = 15;
  public float rotateAccelerationMultiplier = 5;
  public float maxForwardVelocity = 2;
}

[Serializable]
public class CharacterAnimationSettings
{
  public float speed;
  public RuntimeAnimatorController idleController;
  public RuntimeAnimatorController walkController;
  public RuntimeAnimatorController runController;
  // public RuntimeAnimatorController slipController;
  public RuntimeAnimatorController slackController;
  // public RuntimeAnimatorController decelerationController;
}