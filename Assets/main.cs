using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Palmmedia.ReportGenerator.Core.Common;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Main : MonoBehaviour
{
  public TextMeshProUGUI Key;
  public TextMeshProUGUI HP;
  KeyCode currentKeyCode = KeyCode.W;
  int hp = 100;
  static KeyCodeRewire keyCodeRewire = new KeyCodeRewire();
  void Start()
  {
    // print(keyCodeRewire.retrieveCache("KeyboardRewire"));
    // Observer.Subscribe(KeyboardEventType.KeyboardKeyDown, onKeyDown);
  }

  public static void rewire()
  {
    // keyCodeRewire.set(KeyCode.Mouse0, KeyCode.W);
    // print(keyCodeRewire.cache("KeyboardRewire", true));

    print("Rewired!");
  }

  void modifyHP(int by)
  {
    hp += by;

    if (hp < 0) hp = 0;
    if (hp > 100) hp = 100;

    HP.text = hp.ToString();
  }

  KeyCode randomKeyCode()
  {
    int randomInt = RandomNumberGenerator.GetInt32(4);
    switch (randomInt)
    {
      case 0:
        return KeyCode.W;
      case 1:
        return KeyCode.A;
      case 2:
        return KeyCode.S;
      case 3:
        return KeyCode.D;
      default:
        return KeyCode.W;
    }
  }
}

[Serializable]
class KeyCodeRewire
{
  private Dictionary<KeyCode, KeyCode> customKeyCodes = new Dictionary<KeyCode, KeyCode>();

  public KeyCodeRewire() { }
  public KeyCodeRewire(Dictionary<KeyCode, KeyCode> customKeyCodes)
  {
    this.customKeyCodes = new Dictionary<KeyCode, KeyCode>(customKeyCodes);
  }
  public KeyCode get(KeyCode keyCode)
  {
    if (customKeyCodes.ContainsKey(keyCode))
      return customKeyCodes[keyCode];

    return keyCode;
  }

  public void set(KeyCode keyCode, KeyCode customKeyCode)
  {
    customKeyCodes[keyCode] = customKeyCode;
    customKeyCodes[customKeyCode] = keyCode;
  }
  /// <summary>
  /// Caches given values via PlayerPrefs using id
  ///
  /// Writes to disk if should
  /// </summary>
  // public string cache(string id, bool? shouldSave)
  // {
  //   string d = "{";
  //   foreach (KeyValuePair<KeyCode, KeyCode> item in customKeyCodes)
  //   {
  //     d += "\"" + item.Key + "\":" + "\"" + item.Value + "\",";
  //   }
  //   d.Remove(d.Length - 1);
  //   d += "}";

  //   PlayerPrefs.SetString(id, d);
  //   if (shouldSave.ConvertTo<bool>()) PlayerPrefs.Save();

  //   return d;
  // }

  // public object retrieveCache(string id)
  // {
  //   if (!PlayerPrefs.HasKey(id)) return null;

  //   string cache = PlayerPrefs.GetString(id);
  //   Dictionary<KeyCode, KeyCode> cacheDictionary = JsonUtility.FromJson<Dictionary<KeyCode, KeyCode>>(cache);

  //   if (cacheDictionary == null) return null;

  //   customKeyCodes = new Dictionary<KeyCode, KeyCode>(cacheDictionary);

  //   return cacheDictionary;
  // }
}
