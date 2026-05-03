using UnityEngine;

public class PresentationScreen : MonoBehaviour
{
    public GameObject fishPrefab;
    [SerializeField] GameObject _schoolManagerPrefab;
    [SerializeField] private RectTransform _boundingRectTransform;

    void OnEnable()
    {
        foreach (var fishType in SimulationManager.instance.fishInv.Keys)
        {
            GameObject SMGameObject = Instantiate(_schoolManagerPrefab, transform);
            if (SMGameObject.TryGetComponent(out SchoolManager schoolManager))
            {
                schoolManager.presentationScreen = this;
                schoolManager.Initialize(fishType, 10);
            }
        }
    }

    public Vector2 GetRandomPointInBounds(float schoolRadius)
    {
        Vector3[] corners = new Vector3[4];
        _boundingRectTransform.GetWorldCorners(corners);
        float xmin = corners[0].x + schoolRadius;
        float xmax = corners[2].x - schoolRadius;
        float ymin = corners[0].y + schoolRadius;
        float ymax = corners[2].y - schoolRadius;

        float randomx = Random.Range(xmin, xmax);
        float randomy = Random.Range(ymin, ymax);

        return new Vector2(randomx, randomy);
    }

    public Vector3 ClampToBounds(Vector3 inVector)
    {
        Vector3[] corners = new Vector3[4];
        _boundingRectTransform.GetWorldCorners(corners);

        float newX = Mathf.Clamp(inVector.x, corners[0].x, corners[2].x);
        float newY = Mathf.Clamp(inVector.y, corners[0].y, corners[2].y);

        return new Vector3(newX, newY, inVector.z);
    }
}
