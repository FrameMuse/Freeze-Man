using UnityEngine;

[AddComponentMenu("Item/Item Controller")]
[IconAttribute("Assets/Images/Icons/item.svg")]
[RequireComponent(typeof(Renderer), typeof(Collider))]
public class ItemController : MonoBehaviour
{
  [Header("Item settings")]
  [SerializeField]
  public bool pickable = true;
  [SerializeField]
  public IItem.id itemId = IItem.id.other;
  [SerializeField]
  public IItem.type itemType = IItem.type.other;
  Renderer attachedRenderer;
  Collider attachedCollider;

  Material haloMaterial = Static.haloMeterial;
  Material defaultMaterial;
  public Bounds bounds { get => attachedCollider.bounds; }

  bool attachedColliderPreviousEnabled;
  void Start()
  {
    attachedRenderer = GetComponent<Renderer>();
    attachedCollider = GetComponent<Collider>();

    defaultMaterial = attachedRenderer.material;
    attachedColliderPreviousEnabled = attachedCollider.enabled;
  }

  public void DisableCollider()
  {
    if (attachedCollider.enabled == false) return;

    attachedColliderPreviousEnabled = attachedCollider.enabled;
    attachedCollider.enabled = false;
  }

  public void ReEnableCollider() => attachedCollider.enabled = attachedColliderPreviousEnabled;

  public void OnOffHalo(bool onOff)
  {
    if (onOff)
      attachedRenderer.material = Static.haloMeterial;
    else
      attachedRenderer.material = defaultMaterial;
  }
}