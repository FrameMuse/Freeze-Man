using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMove : MonoBehaviour
{
  private new Rigidbody rigidbody;
  float multiplier = 0.001f;

  void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
  }

  void Update()
  {
    // Address move events
    onAxesUpdate();
  }

  void onAxesUpdate()
  {
    Vector3 axes = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    move(axes);
    face(axes);
  }

  /// <summary>
  /// Translate character to a certain values
  /// </summary>
  void move(Vector3 moveAxes)
  {
    transform.position += moveAxes * (multiplier + Time.deltaTime);
  }

  /// <summary>
  /// Rotate character's body to a certain direction
  /// </summary>
  void face(Vector3 faceAxes)
  {
    float angle = 0;
    bool toLeft = faceAxes.x < 0;
    bool toRight = faceAxes.x > 0;
    bool forward = faceAxes.z > 0;
    bool backward = faceAxes.z < 0;
    bool idle = faceAxes.x == 0 && faceAxes.z == 0;

    if (idle) return;

    if (toLeft) angle = -90;
    if (toRight) angle = 90;
    if (forward) angle = 0;
    if (backward) angle = 180;

    Vector3 nextEulerAngles = new(1 + Time.deltaTime, 0, 1 + Time.deltaTime);
    transform.Rotate(nextEulerAngles);
  }

  // float prevAngle;
  // void OnMouseDown()
  // {
  //   Quaternion rotation = transform.rotation;
  //   Vector3 eulerAngles = rotation.eulerAngles;
  //   eulerAngles.z = 0;
  //   rotation.eulerAngles = eulerAngles;
  //   transform.rotation = rotation;
  // }
}
