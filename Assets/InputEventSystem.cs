using UnityEngine;

public class InputEventSystem : MonoBehaviour
{
  void Update()
  {
    // Loop over all the keycodes
    foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
    {
      // Send event to key down
      if (Input.GetKeyDown(keyCode))
        Observer.Dispatch(KeyboardEventType.KeyboardKeyDown, keyCode);

      // Send event to key up
      if (Input.GetKeyUp(keyCode))
        Observer.Dispatch(KeyboardEventType.KeyboardKeyUp, keyCode);

      // Send event to while key is held down
      if (Input.GetKey(keyCode))
        Observer.Dispatch(KeyboardEventType.KeyboardKeyHold, keyCode);
    }
  }
}

public enum KeyboardEventType
{
  KeyboardKeyDown,
  KeyboardKeyUp,
  KeyboardKeyHold
}