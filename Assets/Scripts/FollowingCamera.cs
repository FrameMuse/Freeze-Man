using UnityEngine;

[AddComponentMenu("Following Camera")]
[DisallowMultipleComponent]
public class FollowingCamera : MonoBehaviour
{
  [SerializeField]
  GameObject target;
  [SerializeField]
  public float distanceToTarget = 10f;
  [SerializeField]
  Vector3 offset = new Vector3(0, 0, 0);

  Vector3 previousPosition;

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

  void Follow()
  {
    // float angleSpeed = 100 * Time.deltaTime;

    float xAxis = Input.GetAxis("Mouse X");
    float yAxis = Input.GetAxis("Mouse Y");

    // float xRotation = (xAxis * angleSpeed);
    // float yRotation = transform.localEulerAngles.x + (yAxis * angleSpeed);

    // float xRotationClamped = xRotation;
    // float yRotationClamped = Mathf.Clamp(yRotation, -60, 60);



    // Vector3 newPosition = ScreenToViewportPoint(Input.mousePosition);
    Vector3 direction = new(xAxis, yAxis);

    float rotationAroundYAxis = direction.x; // camera moves horizontally
    float rotationAroundXAxis = -direction.y; // camera moves vertically

    transform.position = target.transform.position;

    transform.Rotate(Vector3.right, rotationAroundXAxis);
    transform.Rotate(Vector3.up, rotationAroundYAxis, Space.World); // <— This is what makes it work!

    transform.Translate(new Vector3(0, 0, -distanceToTarget) + offset);

    Vector3 euler = transform.rotation.eulerAngles;
    euler.x = Mathf.Clamp(euler.x, 15, 65);
    transform.rotation = Quaternion.Euler(euler);

    // previousPosition = newPosition;



    // transform.position = target.transform.position + offset;
    // // transform.RotateAround(target.transform.position, Vector3.up, 20f * Time.deltaTime);
    // transform.Rotate(Vector3.up, 20f * Time.deltaTime, Space.World);
    // transform.LookAt(target.transform.position);


    // Vector3 eulerAngles = new();
    // eulerAngles += Quaternion.AngleAxis(xRotationClamped, Vector3.up).eulerAngles;
    // eulerAngles += Quaternion.AngleAxis(yRotationClamped, Vector3.left).eulerAngles;

    // transform.localRotation = Quaternion.Euler(eulerAngles);
    // transform.localRotation = Quaternion.AngleAxis(yRotationClamped, Vector3.left);

    // transform.LookAt(target.transform.position);

    // transform.Translate()

    // transform.RotateAround(target.transform.position, Vector3.up, (xAxis * angleSpeed));
    // transform.RotateAround(target.transform.position, Vector3.left, (yAxis * angleSpeed));
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
