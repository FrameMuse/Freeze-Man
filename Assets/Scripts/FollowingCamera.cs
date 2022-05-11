using UnityEngine;

[AddComponentMenu("Following Camera")]
public class FollowingCamera : MonoBehaviour
{
  public GameObject toFollow;
  void Update()
  {
    transform.position = toFollow.transform.position + new Vector3(0, 2, -2);
  }
}
