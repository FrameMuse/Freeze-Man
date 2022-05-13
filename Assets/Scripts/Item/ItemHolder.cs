using UnityEngine;

[AddComponentMenu("Item/Item Holder")]
[IconAttribute("Assets/Images/Icons/item-holder.svg")]
public class ItemHolder : MonoBehaviour
{
  [SerializeField]
  public ItemController heldItem;
  [SerializeField]
  Vector3 offset;
  public bool isHolding { get => heldItem != null; }
  Vector3 position;
  void Update()
  {
    if (!isHolding) return;
    // When holding
    heldItem.transform.position = transform.position + offset;
    heldItem.transform.eulerAngles = transform.rotation.eulerAngles;
  }


  public void PickUpItem(ItemController item)
  {
    // Set held item
    heldItem = item;
    // Remove halo
    // heldItem.OnOffHalo(false);
    heldItem.DisableCollider();
  }
  public void ReleaseItem()
  {
    // Remove halo
    // heldItem.OnOffHalo(false);
    // Revert held item collider enabled state to its previous state
    heldItem.ReEnableCollider();
    // Remove held item
    heldItem = null;
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawSphere(position, 0.1f);
    Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 5);
  }
}
