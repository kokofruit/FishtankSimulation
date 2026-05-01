using System.Collections.Generic;
using UnityEngine;

public class SchoolManager : MonoBehaviour
{
    public GameObject[] schoolMembers;
    public float schoolAreaRadius = 100;

    public void Initialize(JSONReader.Fish fishType, int schoolSize)
    {
        schoolMembers = new GameObject[schoolSize];
        for (int i = 0; i < schoolSize; i++)
        {
            // choose a random position based around this thing's postion
            Vector2 position = transform.position;
            position += Random.insideUnitCircle * schoolAreaRadius;

            // create empty fish object
            schoolMembers[i] = Instantiate(SimulationManager.instance.fishPrefab, position, Quaternion.identity);
            if (schoolMembers[i].TryGetComponent(out SchoolMember schoolMember))
            {
                schoolMember.SetFish(fishType);
                schoolMember.schoolManager = this;
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
