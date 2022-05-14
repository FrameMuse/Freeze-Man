using UnityEngine;
using UnityEngine.UIElements;

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
  Rigidbody attachedRigidbody;

  Material haloMaterial = Static.haloMeterial;
  Material defaultMaterial;
  public Bounds bounds { get => attachedCollider.bounds; }

  bool attachedColliderPreviousEnabled;
  bool attachedRigidbodyPreviousUseGravity;
  void Start()
  {
    attachedRenderer = GetComponent<Renderer>();
    attachedCollider = GetComponent<Collider>();
    attachedRigidbody = GetComponent<Rigidbody>();

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

  public void DisableRigidbody()
  {
    if (attachedRigidbody == null) return;
    if (attachedRigidbody.useGravity == false) return;

    attachedRigidbodyPreviousUseGravity = attachedRigidbody.useGravity;
    attachedRigidbody.useGravity = false;
  }
  public void ReEnableRigidbody()
  {
    if (attachedRigidbody == null) return;

    attachedRigidbody.useGravity = attachedRigidbodyPreviousUseGravity;
  }
  public void OnOffHalo(bool onOff)
  {
    if (onOff)
      attachedRenderer.material = Static.haloMeterial;
    else
      attachedRenderer.material = defaultMaterial;
  }

  public string lastTooltip = " ";
  void OnGUI()
  {
    GUILayout.Button(new GUIContent("Play Game", "Button1"));
    GUILayout.Button(new GUIContent("Quit", "Button2"));
    if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTooltip)
    {
      if (lastTooltip != "")
        SendMessage(lastTooltip + "OnMouseOut", SendMessageOptions.DontRequireReceiver);

      if (GUI.tooltip != "")
        SendMessage(GUI.tooltip + "OnMouseOver", SendMessageOptions.DontRequireReceiver);

      lastTooltip = GUI.tooltip;
    }
  }
  void Button1OnMouseOver()
  {
    Debug.Log("Play game got focus");
  }
  void Button2OnMouseOut()
  {
    Debug.Log("Quit lost focus");
  }
}
