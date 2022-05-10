using UnityEngine;

[AddComponentMenu("Helper/Center of Mass")]
[RequireComponent(typeof(Rigidbody))]
public class CenterOfMass : MonoBehaviour
{
  public Vector3 massPosition;
  public int gizmosSphereRadius = 10;
  public bool Awake;
  protected new Rigidbody rigidbody;
  void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
  }
  void Update()
  {
    rigidbody.centerOfMass = massPosition;
    rigidbody.WakeUp();
    Awake = !rigidbody.IsSleeping();
  }


  private void OnDrawGizmos()
  {
    Gizmos.color = Color.cyan;
    Gizmos.DrawSphere(transform.position + (transform.rotation * massPosition), gizmosSphereRadius / 1000f);
  }

  // private float getGizmosSphereRadius()
  // {
  //   float mass = rigidbody != null ? rigidbody.mass : 1;
  //   return transform.localScale.z * (mass * massMultiplier);
  // }
}