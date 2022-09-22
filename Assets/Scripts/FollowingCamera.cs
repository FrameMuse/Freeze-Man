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
  float floatingSpeed = 3f;

  Vector3 previousPosition;

  Vector3 velocity = Vector3.zero;

  void Start()
  {
    Cursor.visible = false;

    // transform.LookAt(target.transform.position);
  }
  void Update()
  {
    Follow();
    LockCursorIfNeeded();

    distanceToTarget -= Input.GetAxis("Mouse ScrollWheel");
  }
  Vector3 asd;
  Vector3 asd2;
  public float smoothTime = 0.3F;
  void Follow()
  {
    float xAxis = Input.GetAxis("Mouse X");
    float yAxis = Input.GetAxis("Mouse Y");


    Vector3 accelerationDirection = new(xAxis, yAxis);

    float rotationAroundYAxis = accelerationDirection.x; // camera moves horizontally
    float rotationAroundXAxis = -accelerationDirection.y; // camera moves vertically

    // Debug.Log(new { asd2, target.transform.position });


    // Set target's position
    // Vector3 targetPosition = target.transform.TransformPoint(new Vector3(0, 5, -10));
    transform.position = Vector3.SmoothDamp(asd2, target.transform.position, ref velocity, smoothTime);
    // transform.position = Vector3.SmoothDamp(asd2, target.transform.position, floatingSpeed * Time.deltaTime * ((asd2 - target.transform.position).magnitude));
    asd2 = transform.position;

    transform.Rotate(Vector3.right, rotationAroundXAxis);
    transform.Rotate(Vector3.up, rotationAroundYAxis, Space.World); // <— This is what makes it work!

    transform.Translate(new Vector3(0, 0, -distanceToTarget) + offset);

    // Clamping rotation x
    Vector3 euler = transform.rotation.eulerAngles;
    euler.x = Mathf.Clamp(euler.x, 5, 65);
    transform.rotation = Quaternion.Euler(euler);
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
}
