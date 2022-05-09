using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
  public GameObject body;
  // Start is called before the first frame update
  void Start()
  {
    // Observer.Subscribe(InputEventType.VerticalAxis, onVerticalAxis);
    // Observer.Subscribe(InputEventType.HorizontalAxis, onHorizontalAxis);
  }
  float multiplier = 0.01f;

  void Update()
  {
    Vector3 moveAxes = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    body.transform.position += moveAxes * (multiplier + Time.deltaTime);
  }


  void onVerticalAxis(object payload)
  {
    float axis = (float)payload;

    Vector3 moveAxis = new Vector3(0, 0, axis);
    Vector3 moveDirection = new Vector3(0, 0, 1f);

    body.transform.position += moveAxis * (multiplier + Time.deltaTime);
  }
  void onHorizontalAxis(object payload)
  {
    float axis = (float)payload;

    Vector3 moveAxis = new Vector3(axis, 0, 0);
    Vector3 moveDirection = new Vector3(1f, 0, 0);

    body.transform.position += moveAxis * (multiplier + Time.deltaTime);
    // if (keyCode == KeyCode.W)
    // {
    //   body.transform.position = new Vector3(position.x, position.y, position.z + Time.fixedDeltaTime);
    // }
    // if (keyCode == KeyCode.S)
    // {
    //   body.transform.position = new Vector3(position.x, position.y, position.z - Time.fixedDeltaTime);
    // }
    // if (keyCode == KeyCode.D)
    // {
    //   body.transform.position = new Vector3(position.x + Time.fixedDeltaTime, position.y, position.z);
    // }

    // if (keyCode == KeyCode.A)
    // {
    //   body.transform.position = new Vector3(position.x - Time.fixedDeltaTime, position.y, position.z);
    // }

    // if (keyCode == KeyCode.Space)
    // {
    //   body.transform.position = new Vector3(position.x, position.y + Time.fixedDeltaTime, position.z);
    // }

    // if (keyCode == KeyCode.LeftShift || keyCode == KeyCode.RightShift)
    // {
    //   body.transform.position = new Vector3(position.x, position.y - Time.fixedDeltaTime, position.z);
    // }
  }

  void OnMouseDown()
  {
    print("Rewired!");
    // Main.rewire();
  }
}
