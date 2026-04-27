using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class DraggableObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // Based on code from https://medium.com/medialesson/drag-drop-for-ui-elements-in-unity-the-simple-ish-way-9efcb4617648

    private DraggingManager _draggingManager;
    private Vector3 _centerPoint;
    private Vector2 _worldCenterPoint => transform.TransformPoint(_centerPoint);
    private Vector2 _rectSize;

    private void Awake()
    {
        // find the dragging manager
        _draggingManager = FindAnyObjectByType<DraggingManager>();
        // get dimensions
        GetDimensions();
    }

    private void GetDimensions()
    {
        // temporarily cache recttransform
        RectTransform rectTransform = GetComponent<RectTransform>();
        // grab the center point
        _centerPoint = rectTransform.rect.center;
        // get rectangle size
        _rectSize = rectTransform.rect.size * transform.lossyScale;
    }

    public void SetDecor(JSONReader.Decoration decoration)
    {
        Image image = GetComponent<Image>();
        // TODO: Set scale !!!!!!!
        transform.localScale = Vector3.one;
        // Set image
        Sprite sprite = Resources.Load<Sprite>("Images/Decor/" + decoration.name);
        image.sprite = sprite;
        // Recalculate dimensions
        GetDimensions();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _draggingManager.StartDraggingObject(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // if the entire rectangle will be within bounds, move the rectangle
        if (_draggingManager.IsWithinBounds(_worldCenterPoint - _rectSize / 2 + eventData.delta)
        && _draggingManager.IsWithinBounds(_worldCenterPoint + _rectSize / 2 + eventData.delta))
            transform.Translate(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _draggingManager.StopDraggingObject(this);
    }
}
