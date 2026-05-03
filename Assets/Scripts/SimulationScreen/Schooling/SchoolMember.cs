// written using https://learn.unity.com/tutorial/flocking

using UnityEngine;

public class SchoolMember : MonoBehaviour
{
    // the manager of this fish's school
    public SchoolManager schoolManager;
    // the overall screen manager
    private PresentationScreen _presentationScreen;

    // the current speed for this fish
    public float speed;

    public void SetFish(JSONReader.Fish fish)
    {
        speed = Random.Range(schoolManager.minSpeed, schoolManager.maxSpeed);
        _presentationScreen = schoolManager.presentationScreen;
    }

    // Update is called once per frame
    void Update()
    {
        // exit if there's no school manager, which shouldn't be possible, but,,,, y'know
        if (!schoolManager) return;

        // random chance to apply the rules
        if (Random.value < 0.5f) ApplyRules();
        // random chance to vary the speed
        if (Random.value < 0.1f) speed = Random.Range(schoolManager.minSpeed, schoolManager.maxSpeed);

        // move in the current direction
        transform.Translate(0, 0, speed * Time.deltaTime);
        transform.position = _presentationScreen.ClampToBounds(transform.position);
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

        // process for all neighbors except itself
        foreach (GameObject neighbor in neighbors)
        {
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

        // if there is a school, do some calculations
        if (groupSize > 0)
        {
            vCenter = vCenter / groupSize + schoolManager.transform.position - transform.position;
            speed = Mathf.Clamp(groupSpeed / groupSize, schoolManager.minSpeed, schoolManager.maxSpeed);

            Vector3 direction = vCenter + vAvoid - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), schoolManager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
