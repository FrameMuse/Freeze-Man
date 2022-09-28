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
  RuntimeAnimatorController stopController;

  Animator animator;

  void Start()
  {
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    animator.speed = speed;
    Vector3 accelerationAxes = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    if (accelerationAxes.magnitude > 0)
    {
      if (Input.GetKey(KeyCode.LeftShift))
      {
        animator.runtimeAnimatorController = runController;
        return;
      }

      animator.runtimeAnimatorController = walkController;
      return;
    }


    // Idle
    animator.runtimeAnimatorController = idleController;
  }
}