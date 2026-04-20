using UnityEngine;

public class FishController : MonoBehaviour
{
    FishManager _fishManager;
    RectTransform _rectTransform;

    void Awake()
    {
        _fishManager = FindFirstObjectByType<FishManager>();
        _rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        InvokeRepeating(nameof(MoveToRandomPoint), 0.5f, 1f);
    }

    private void MoveToRandomPoint()
    {
        transform.position = _fishManager.GetRandomPointInBounds(_rectTransform.rect.size * transform.lossyScale);
    }
}
