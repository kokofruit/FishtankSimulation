using System.Collections;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public float swimSpeed;
    public float idleTime;

    FishManager _fishManager;
    RectTransform _rectTransform;

    void Awake()
    {
        _fishManager = FindFirstObjectByType<FishManager>();
        _rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        // InvokeRepeating(nameof(MoveToRandomPoint), 0.5f, 1f);
        StartCoroutine("MoveToRandomPoint");
    }

    private IEnumerator MoveToRandomPoint()
    {
        Vector3 destination = _fishManager.GetRandomPointInBounds(_rectTransform.rect.size * transform.lossyScale);
        while (true)
        {
            if (Vector3.Distance(transform.position, destination) < 0.5f)
            {
                print("this is firing");
                yield return new WaitForSeconds(idleTime);
                destination = _fishManager.GetRandomPointInBounds(_rectTransform.rect.size * transform.lossyScale);
            }
            else
            {
                print("dest:" + destination + " pos:" + transform.position);
                transform.position = Vector3.MoveTowards(transform.position, destination, swimSpeed * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
