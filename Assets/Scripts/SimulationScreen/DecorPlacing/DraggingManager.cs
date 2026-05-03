using UnityEngine;
using UnityEngine.UI;

public class DraggingManager : MonoBehaviour
{
    // Based on code from https://medium.com/medialesson/drag-drop-for-ui-elements-in-unity-the-simple-ish-way-9efcb4617648

    [SerializeField] private RectTransform _defaultLayer;
    [SerializeField] private RectTransform _dragLayer;
    [SerializeField] private RectTransform _finalLayer;

    [SerializeField] private Rect _boundingBox;

    [SerializeField] private GameObject _draggablePrefab;
    [SerializeField] private Image _substrateImage;


    public DraggableObject currentDraggable;


    private void OnEnable()
    {
        SetBoundingBoxRect();

        foreach (JSONReader.Decoration decoration in SimulationManager.instance.decorationInventory)
        {
            DraggableObject draggableObject = Instantiate(_draggablePrefab, _boundingBox.center, Quaternion.identity).GetComponent<DraggableObject>();
            draggableObject.transform.SetParent(_defaultLayer);
            draggableObject.transform.localScale = Vector3.one;
            draggableObject.SetDecor(decoration);
        }

        _substrateImage.sprite = Resources.Load<Sprite>("Images/Display/Substrate/" + SimulationManager.instance.substrateInventory.id);
    }

    public void StartDraggingObject(DraggableObject draggedObject)
    {
        currentDraggable = draggedObject;
        draggedObject.transform.SetParent(_dragLayer);
    }

    public void StopDraggingObject(DraggableObject draggedObject)
    {
        draggedObject.transform.SetParent(_defaultLayer);
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

    public void NextScreen()
    {
        foreach (DraggableObject draggable in FindObjectsByType<DraggableObject>(FindObjectsSortMode.None))
        {
            draggable.transform.SetParent(_finalLayer);
            Destroy(draggable);
        }
        _substrateImage.transform.SetParent(_finalLayer);
        SimulationManager.instance.NextScreen();
    }

    public void PreviousScreen()
    {
        foreach (DraggableObject draggable in FindObjectsByType<DraggableObject>(FindObjectsSortMode.None))
        {
            Destroy(draggable.gameObject);
        }
        SimulationManager.instance.PreviousScreen();
    }
}
