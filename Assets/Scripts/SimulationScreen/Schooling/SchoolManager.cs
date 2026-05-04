// written using https://learn.unity.com/tutorial/flocking

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolManager : MonoBehaviour
{
    // the overall screen manager
    public PresentationScreen presentationScreen;
    
    [Header("School Variables")]
    // the members of this school
    public GameObject[] schoolMembers;
    // the spawn radius for this school
    public float schoolAreaRadius = 100;
    // the goal position of this school
    public Vector3 goalPos;
    // the movement speed of the overall group
    public float groupMoveSpeed = 20f;

    [Header("Fish Variables")]
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
            schoolMembers[i] = Instantiate(presentationScreen.fishPrefab, position, Quaternion.identity, transform);
            if (schoolMembers[i].TryGetComponent(out SchoolMember schoolMember))
            {
                schoolMember.schoolManager = this;
                schoolMember.SetFish(fishType);
            }
        }
        goalPos = transform.position;

        StartCoroutine(nameof(MoveToRandomPoint));
    }

    private IEnumerator MoveToRandomPoint()
    {
        goalPos = presentationScreen.GetRandomPointInBounds(schoolAreaRadius);
        while (true)
        {
            if (Vector3.Distance(transform.position, goalPos) < 0.5f)
            {
                goalPos = presentationScreen.GetRandomPointInBounds(schoolAreaRadius);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, goalPos, groupMoveSpeed * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
