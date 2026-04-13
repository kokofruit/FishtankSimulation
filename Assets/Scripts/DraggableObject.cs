using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // Based on code from https://medium.com/medialesson/drag-drop-for-ui-elements-in-unity-the-simple-ish-way-9efcb4617648

    private DraggingManager _draggingManager;
    private Vector3 _centerPoint;
    private Vector2 _worldCenterPoint => transform.TransformPoint(_centerPoint);

    private void Awake()
    {
        _draggingManager = GetComponentInParent<DraggingManager>();
        _centerPoint = GetComponent<RectTransform>().rect.center;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _draggingManager.StartDraggingObject(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_draggingManager.IsWithinBounds(_worldCenterPoint + eventData.delta)) transform.Translate(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _draggingManager.StopDraggingObject(this);
        print(eventData.position);
    }
}
