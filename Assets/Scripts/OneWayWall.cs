using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayWall : MonoBehaviour
{
  new Collider collider;
  public bool clearColliders;
  public List<Collider> ignoredColliders = new();
  void Start()
  {
    collider = GetComponent<Collider>();
  }

  void OnCollisionEnter(Collision collision)
  {
    Debug.Log(collision.collider.name);


    // Physics.IgnoreCollision(collider, collision.collider);
    // ignoredColliders.Add(collision.collider);
  }

  // void Update()
  // {
  //   if (clearColliders)
  //   {
  //     ClearIgnoredColliders();

  //     clearColliders = !clearColliders;
  //   }
  // }

  // void ClearIgnoredColliders()
  // {
  //   ignoredColliders.ForEach(UnignoredCollider);
  //   ignoredColliders = new();
  // }

  // void UnignoredCollider(Collider ignoredCollider)
  // {
  //   Physics.IgnoreCollision(collider, ignoredCollider, false);
  // }
}
