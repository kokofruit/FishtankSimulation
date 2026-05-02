using UnityEngine;

public class SchoolMember : MonoBehaviour
{
    public SchoolManager schoolManager;

    public float speed;

    public void SetFish(JSONReader.Fish fish)
    {
        speed = Random.Range(schoolManager.minSpeed, schoolManager.maxSpeed);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (schoolManager)
        {
            ApplyRules();
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    void ApplyRules()
    {
        // get all neighbors
        GameObject[] neighbors = schoolManager.schoolMembers;

        // initialize variables
        Vector3 vCenter = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        float groupSpeed = 0.01f;
        int groupSize = 0;

        foreach (GameObject neighbor in neighbors)
        {
            // exit if this is the same one
            if (neighbor != this.gameObject)
            {
                // calculate distance
                float neighborDistance = Vector3.Distance(transform.position, neighbor.transform.position);
                if (neighborDistance <= schoolManager.neighborDistance)
                {
                    // track neighbors' position
                    vCenter += neighbor.transform.position;
                    groupSize++;

                    // track neighbors to avoid
                    if (neighborDistance < schoolManager.proxDistance)
                    {
                        vAvoid += transform.position - neighbor.transform.position;
                    }

                    // track group's speed
                    SchoolMember otherMember = neighbor.GetComponent<SchoolMember>();
                    groupSpeed += otherMember.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            vCenter = vCenter / groupSize;
            speed = groupSpeed / groupSize;

            Vector3 direction = (vCenter + vAvoid - transform.position);
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), schoolManager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
