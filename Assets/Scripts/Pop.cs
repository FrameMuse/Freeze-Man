using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Pop : MonoBehaviour
{
  private Animator animator;
  void Start()
  {
    animator = GetComponent<Animator>();
    animator.enabled = false;
  }

  void Update()
  {

  }

  void OnMouseDown()
  {
    animator.enabled = true;

    animator.Rebind();
    animator.Update(0f);
  }
}
