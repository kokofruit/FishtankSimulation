using UnityEngine;
using UnityEngine.UI;

public class DraggingManager : MonoBehaviour
{
    // Based on code from https://medium.com/medialesson/drag-drop-for-ui-elements-in-unity-the-simple-ish-way-9efcb4617648

    // the rect transforms of different layers
    [SerializeField] private RectTransform _defaultLayer;
    [SerializeField] private RectTransform _dragLayer;
    [SerializeField] private RectTransform _finalLayer;

    // the constraints for dragging
    private Rect _boundingBox;

    // the prefab for draggable decor
    [SerializeField] private GameObject _draggablePrefab;
    // the current dragged object
    public DraggableObject currentDraggable;

    // the image for the substrate
    [SerializeField] private Image _substrateImage;

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
        // move the substrate image to the final layer
        _substrateImage.transform.SetParent(_finalLayer);
        // move each decor image to the final layer
        foreach (DraggableObject draggable in FindObjectsByType<DraggableObject>(FindObjectsSortMode.None))
        {
            draggable.transform.SetParent(_finalLayer);
            Destroy(draggable);
        }
        // go to the next screen
        SimulationManager.instance.NextScreen();
    }

    public void PreviousScreen()
    {
        // destroy each draggable since they'll be recreated
        foreach (DraggableObject draggable in FindObjectsByType<DraggableObject>(FindObjectsSortMode.None))
        {
            Destroy(draggable.gameObject);
        }
        // go to the last screen
        SimulationManager.instance.PreviousScreen();
    }
}
