using System;
using UnityEngine;

[AddComponentMenu("Following Camera")]
[DisallowMultipleComponent]
public class FollowingCamera : MonoBehaviour
{
  [SerializeField]
  GameObject target;
  [SerializeField]
  float distanceToTarget = 10f;
  [SerializeField]
  Vector3 offset = new Vector3(0, 0, 0);
  [SerializeField]
  float smoothTime = 0.3f;

  Vector3 velocity = Vector3.zero;
  Vector3 previousPosition;

  void Start()
  {
    // Physics.IgnoreCollision(transform)
  }

  void Update()
  {
    Follow();
    LockCursorIfNeeded();

    distanceToTarget -= Input.GetAxis("Mouse ScrollWheel");

    Debug.Log(GetNearestOverlappingCollider());
    // Debug.Log(Physics.OverlapSphere(transform.position, distanceToTarget / 2));
  }
  void Follow()
  {
    float xAxis = Input.GetAxis("Mouse X");
    float yAxis = Input.GetAxis("Mouse Y");


    Vector3 accelerationDirection = new(xAxis, yAxis);

    float rotationAroundYAxis = accelerationDirection.x; // camera moves horizontally
    float rotationAroundXAxis = -accelerationDirection.y; // camera moves vertically

    // Set target's position
    transform.position = Vector3.SmoothDamp(previousPosition, target.transform.position, ref velocity, smoothTime);
    previousPosition = transform.position;

    transform.Rotate(Vector3.right, rotationAroundXAxis);
    transform.Rotate(Vector3.up, rotationAroundYAxis, Space.World); // <— This is what makes it work!
    // Clamping rotation x
    Vector3 euler = transform.rotation.eulerAngles;
    euler.x = Mathf.Clamp(euler.x, 15, 65);
    transform.rotation = Quaternion.Euler(euler);

    transform.Translate(new Vector3(0, 0, -distanceToTarget) + offset);

  }

  void LockCursorIfNeeded()
  {
    Cursor.lockState = CursorLockMode.Locked;
    if (Input.GetKey(KeyCode.F))
      Cursor.lockState = CursorLockMode.None;
  }

  public Vector3 GetLookDirection()
  {
    return (target.transform.position - transform.position) / distanceToTarget;
  }

#nullable enable
  public Collider? GetNearestOverlappingCollider()
  {
    Collider[] colliders = Physics.OverlapSphere(transform.position, distanceToTarget / 2);
    Collider? nearestCollider = null;
    float nearestDistance = Mathf.Infinity;

    foreach (Collider collider in colliders)
    {
      float distance = Vector3.Distance(transform.position, collider.transform.position);

      if (nearestDistance < distance) nearestCollider = collider;
    }

    return nearestCollider;
  }

  void OnDrawGizmos()
  {
    Gizmos.DrawSphere(transform.position, distanceToTarget / 2);
  }
}
