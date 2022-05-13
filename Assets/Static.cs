using UnityEngine;

public class Static : MonoBehaviour
{
  [SerializeField]
  Material _haloMeterial;
  public static Material haloMeterial;
  void Update()
  {
    Static.haloMeterial = _haloMeterial;
  }
}
