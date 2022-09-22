using UnityEngine;

[AddComponentMenu("Character/Character Movement Animator")]
[RequireComponent(typeof(CharacterController))]
[DisallowMultipleComponent]
public class CharacterMovementAnimator : MonoBehaviour
{
  [SerializeField]
  float speed;
  [SerializeField]
  RuntimeAnimatorController idleController;
  [SerializeField]
  RuntimeAnimatorController walkController;
  [SerializeField]
  RuntimeAnimatorController runController;
  [SerializeField]
  RuntimeAnimatorController slackController;

  Animator animator;

  void Start()
  {
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    Vector3 accelerationAxes = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    // Walk
    if (accelerationAxes.magnitude > 0)
    {
      animator.runtimeAnimatorController = walkController;
      return;
    }

    // Idle
    animator.runtimeAnimatorController = idleController;
  }
}