using UnityEngine;

[AddComponentMenu("Item/Item Holder")]
[IconAttribute("Assets/Images/Icons/item-holder.svg")]
[DisallowMultipleComponent]
public class ItemHolder : MonoBehaviour
{
  [SerializeField]
  public ItemController heldItem;
  [SerializeField]
  Vector3 offset;
  public bool isHolding { get => heldItem != null; }
  void Update()
  {
    if (!isHolding) return;
    // When holding
    heldItem.transform.position = Vector3.MoveTowards(heldItem.transform.position, transform.position + -offset, 0.05f);
    heldItem.transform.eulerAngles = transform.rotation.eulerAngles;
  }


  public void PickUpItem(ItemController item)
  {
    // Set held item
    heldItem = item;
    // Remove halo
    // heldItem.OnOffHalo(false);
    heldItem.DisableCollider();
    heldItem.DisableRigidbody();
  }
  public void ReleaseItem()
  {
    // Remove halo
    // heldItem.OnOffHalo(false);
    // Revert held item collider enabled state to its previous state
    heldItem.ReEnableCollider();
    heldItem.ReEnableRigidbody();
    // Remove held item
    heldItem = null;
  }
}
