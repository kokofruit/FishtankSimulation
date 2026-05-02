using System.Collections.Generic;
using UnityEngine;

public class SchoolManager : MonoBehaviour
{
    public GameObject[] schoolMembers;
    public float schoolAreaRadius = 100;

    // child vars
    public float minSpeed = 30f;
    public float maxSpeed = 80f;
    public float neighborDistance = 30f;
    public float proxDistance = 10f;
    public float rotationSpeed = 10f;

    public void Initialize(JSONReader.Fish fishType, int schoolSize)
    {
        schoolMembers = new GameObject[schoolSize];
        for (int i = 0; i < schoolSize; i++)
        {
            // choose a random position based around this thing's postion
            Vector2 position = transform.position;
            position += Random.insideUnitCircle * schoolAreaRadius;

            // create empty fish object
            schoolMembers[i] = Instantiate(SimulationManager.instance.fishPrefab, position, Quaternion.identity, transform);
            if (schoolMembers[i].TryGetComponent(out SchoolMember schoolMember))
            {
                schoolMember.schoolManager = this;
                schoolMember.SetFish(fishType);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
