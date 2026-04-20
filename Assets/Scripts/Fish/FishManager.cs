using UnityEngine;

public class FishManager : MonoBehaviour
{
    [SerializeField] private RectTransform _boundingRectTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector2 GetRandomPointInBounds(Vector2 fishSize)
    {
        Vector3[] corners = new Vector3[4];
        _boundingRectTransform.GetWorldCorners(corners);
        float xmin = corners[0].x + (fishSize.x / 2);
        float xmax = corners[2].x - (fishSize.x / 2);
        float ymin = corners[0].y + (fishSize.y / 2);
        float ymax = corners[2].y - (fishSize.y / 2);

        float randomx = Random.Range(xmin, xmax);
        float randomy = Random.Range(ymin, ymax);

        return new Vector2(randomx, randomy);
    }
}
