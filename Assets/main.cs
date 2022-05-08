using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class main : MonoBehaviour
{
  public TextMeshProUGUI Key;
  public TextMeshProUGUI HP;
  string key = "w";
  int hp = 100;
  // Start is called before the first frame update
  void Start()
  {
    print("Start");
  }


  void OnGUI()
  {

    if (Input.anyKeyDown)
    {
      handleAnyKeyDown();
    }
  }

  void handleAnyKeyDown()
  {
    if (Input.GetKeyDown(key))
    {
      key = randomKeyCode();
      Key.text = key.ToUpper();

      modifyHP(1);
      return;
    }

    modifyHP(-5);
  }

  void modifyHP(int by)
  {
    hp += by;

    if (hp < 0) hp = 0;
    if (hp > 100) hp = 100;

    HP.text = hp.ToString();
  }

  string randomKeyCode()
  {
    int randomInt = RandomNumberGenerator.GetInt32(3);
    switch (randomInt)
    {
      case 0:
        return "w";
      case 1:
        return "a";
      case 2:
        return "s";
      default:
        return "d";
    }
  }

  //   void onKeyPress(string key, Action callback)
  //   {
  //     if (Input.GetKey(key)) callback();
  //   }
  //   void onKeyPress(KeyCode key, Action callback)
  //   {
  //     if (Input.GetKeyDown(key)) callback();
  //   }

  //   void OnGUI()
  //   {
  //     onKeyPress(KeyCode.W, () => move(0, 0, 0.01f));
  //     onKeyPress(KeyCode.S, () => move(0, 0, -0.01f));
  //     onKeyPress(KeyCode.A, () => move(-0.01f, 0, 0));
  //     onKeyPress(KeyCode.D, () => move(0.01f, 0, 0));
  //   }

  //   void move(float x, float y, float z)
  //   {
  //     transform.position = new Vector3
  //     {
  //       x = transform.position.x + x,
  //       y = transform.position.y + y,
  //       z = transform.position.z + z
  //     };
  //   }
}
