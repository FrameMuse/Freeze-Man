using UnityEngine;

[AddComponentMenu("Event/Input Event System")]
[DisallowMultipleComponent]
public class InputEventSystem : MonoBehaviour
{
  void Update()
  {
    Observer.Dispatch(InputEventType.VerticalAxis, Input.GetAxis("Vertical"));
    Observer.Dispatch(InputEventType.HorizontalAxis, Input.GetAxis("Horizontal"));
    // Loop over all the keycodes
    foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
    {
      // Send event to key down
      if (Input.GetKeyDown(keyCode))
        Observer.Dispatch(InputEventType.KeyboardKeyDown, keyCode);

      // Send event to key up
      if (Input.GetKeyUp(keyCode))
        Observer.Dispatch(InputEventType.KeyboardKeyUp, keyCode);

      // Send event to while key is held down
      if (Input.GetKey(keyCode))
        Observer.Dispatch(InputEventType.KeyboardKeyHold, keyCode);
    }
  }

  public class asd { }
}

public enum InputEventType
{
  KeyboardKeyDown,
  KeyboardKeyUp,
  KeyboardKeyHold,
  VerticalAxis,
  HorizontalAxis
}