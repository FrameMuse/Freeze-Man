using UnityEngine;

[AddComponentMenu("Following Camera")]
[DisallowMultipleComponent]
public class FollowingCamera : MonoBehaviour
{
  [SerializeField]
  GameObject target;
  [SerializeField]
  Vector3 position = new Vector3(0, 2, -2);

  void Start()
  {
    Cursor.visible = false;
  }
  void Update()
  {
    float xAxis = Input.GetAxis("Mouse X");
    float yAxis = Input.GetAxis("Mouse Y");


    transform.position = target.transform.position + position;
    // transform.RotateAround(target.transform.position, Vector3.up, xAxis * 100 * Time.deltaTime);
    // transform.RotateAround(target.transform.position, Vector3.left, yAxis * 100 * Time.deltaTime);

    Cursor.lockState = CursorLockMode.Locked;
    if (Input.GetKey(KeyCode.F))
      Cursor.lockState = CursorLockMode.None;
  }
}
