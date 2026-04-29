using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{

    public static FlockManager FM;
    public List<GameObject> fishPrefabs;
    public int numFish = 20;
    public GameObject[] allFish;
    public Vector2 swimLimits = new Vector2(5.0f, 5.0f);
    public Vector2 goalPos = Vector2.zero;

    [Header("Fish Settings")]
    [Range(0.0f, 5.0f)] public float minSpeed;
    [Range(0.0f, 5.0f)] public float maxSpeed;
    [Range(1.0f, 10.0f)] public float neighbourDistance;
    [Range(1.0f, 5.0f)] public float rotationSpeed;

    void Start()
    {

        allFish = new GameObject[numFish];

        for (int i = 0; i < numFish; ++i)
        {

            Vector2 pos = (Vector2)this.transform.position + new Vector2(
                Random.Range(-swimLimits.x, swimLimits.x),
                Random.Range(-swimLimits.y, swimLimits.y)
                );

            GameObject prefab = fishPrefabs[Random.Range(0, fishPrefabs.Count)];

            allFish[i] = Instantiate(prefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        }

        FM = this;
        goalPos = this.transform.position;
    }


    void Update()
    {

        if (Random.Range(0, 100) < 10)
        {

            goalPos = (Vector2)this.transform.position + new Vector2(
                Random.Range(-swimLimits.x, swimLimits.x),
                Random.Range(-swimLimits.y, swimLimits.y));
        }
    }
}