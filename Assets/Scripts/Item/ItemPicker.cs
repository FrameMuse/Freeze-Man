using UnityEngine;

[AddComponentMenu("Item/Item Picker")]
[IconAttribute("Assets/Images/Icons/item-picker.svg")]
[DisallowMultipleComponent]
public class ItemPicker : MonoBehaviour
{
  [SerializeField]
  public ItemHolder itemHolder;
  [SerializeField]
  KeyCode pickUpReleaseKey = KeyCode.E;
  [Header("Pick Area settings")]
  [SerializeField]
  Vector3 extents = new(1, 1, 1);
  [SerializeField]
  Vector3 center = new();
  Bounds bounds { get => new(transform.position + center, extents * 2); }
  ItemController[] items;
  void GetItems() => items = FindObjectsOfType<ItemController>();
  void Start()
  {
    // Get all item controllers
    GetItems();
  }
  void OnHierarchyChange()
  {
    // Get all item controllers
    GetItems();
  }

  void FixedUpdate()
  {
    HighlightClosestItem();
  }
  void Update()
  {
    if (Input.GetKeyDown(pickUpReleaseKey))
    {
      GetItems();

      if (itemHolder.isHolding)
        itemHolder.ReleaseItem();
      else
        PickUpClosestItem();
    }
  }

  public void PickUpClosestItem()
  {
    foreach (ItemController item in items)
    {
      if (item.pickable == false) continue;
      if (item.bounds.Intersects(bounds))
      {
        itemHolder.PickUpItem(item);
        return;
      }
    }
  }

  void HighlightClosestItem()
  {
    foreach (ItemController item in items)
    {
      if (itemHolder.heldItem == item)
      {
        item.OnOffHalo(false);
        continue;
      }

      if (item.pickable == false) continue;
      item.OnOffHalo(item.bounds.Intersects(bounds));
    }
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.green;
    Gizmos.DrawWireCube(bounds.center, bounds.extents * 2);
  }
}
