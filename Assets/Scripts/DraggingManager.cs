using UnityEngine;

public class DraggingManager : MonoBehaviour
{
    // Based on code from https://medium.com/medialesson/drag-drop-for-ui-elements-in-unity-the-simple-ish-way-9efcb4617648

    [SerializeField] private RectTransform _defaultLayer;
    [SerializeField] private RectTransform _dragLayer;

    [SerializeField] private Rect _boundingBox;

    public DraggableObject currentDraggable;


    private void OnEnable()
    {
        SetBoundingBoxRect();
    }

    public void StartDraggingObject(DraggableObject draggedObject)
    {
        currentDraggable = draggedObject;
        draggedObject.transform.SetParent(_dragLayer);
    }

    public void StopDraggingObject(DraggableObject draggedObject)
    {
        draggedObject.transform.SetParent(_defaultLayer);
        print(draggedObject.transform.localPosition);
        print(draggedObject.transform.position);
        currentDraggable = null;
    }

    public bool IsWithinBounds(Vector2 position)
    {
        return _boundingBox.Contains(position);
    }

    private void SetBoundingBoxRect()
    {
        Vector3[] corners = new Vector3[4];
        _dragLayer.GetWorldCorners(corners);
        Vector3 position = corners[0];

        float size_x = _dragLayer.lossyScale.x * _dragLayer.rect.size.x;
        float size_y = _dragLayer.lossyScale.y * _dragLayer.rect.size.y;
        Vector2 size = new Vector2(size_x, size_y);

        _boundingBox = new Rect(position, size);
    }
}
