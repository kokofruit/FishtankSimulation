using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Flock : MonoBehaviour
{

    float speed;
    Vector2 moveDirection;
    bool turning = false;

    void Start()
    {

        speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed);
    }


    void Update()
    {
        Vector2 position = transform.position;
        Vector2 center = FlockManager.FM.transform.position;
        Vector2 limits = FlockManager.FM.swimLimits;
        

        if (Mathf.Abs(position.x - center.x) > limits.x || Mathf.Abs(position.y - center.y) > limits.y)
        {

            turning = true;
        }
        else
        {

            turning = false;
        }

        if (turning)
        {

            Vector2 directionToCenter = (center - position).normalized;
            moveDirection = Vector2.Lerp(moveDirection, directionToCenter, speed);
        }
        else
        {


            if (Random.Range(0, 100) < 10)
            {

                speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed);
            }


            if (Random.Range(0, 100) < 10)
            {
                ApplyRules();
            }
        }

        moveDirection.Normalize();

        Flip(moveDirection);

        this.transform.Translate(moveDirection* speed * Time.deltaTime);
    }

    void Flip(Vector2 direction)
    {
        if (direction.x > 0.01f)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else if (direction.x < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void ApplyRules()
    {

        GameObject[] gos;
        gos = FlockManager.FM.allFish;

        Vector2 vCentre = Vector2.zero;
        Vector2 vAvoid = Vector2.zero;

        float gSpeed = 0.01f;
        float mDistance;
        int groupSize = 0;

        foreach (GameObject go in gos)
        {

            if (go != this.gameObject)
            {
                Vector2 otherPosition = go.transform.position;
                Vector2 myPosition = transform.position;

                mDistance = Vector2.Distance(otherPosition, myPosition);

                if (mDistance <= FlockManager.FM.neighbourDistance)
                {

                    vCentre += otherPosition;
                    groupSize++;

                    if (mDistance < 1.0f)
                    {

                        vAvoid += (myPosition - otherPosition);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed += anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            Vector2 myPosition = transform.position;

            vCentre = vCentre / groupSize + (FlockManager.FM.goalPos - myPosition);
            speed = gSpeed / groupSize;

            if (speed > FlockManager.FM.maxSpeed)
            {

                speed = FlockManager.FM.maxSpeed;
            }

            Vector2 direction = (vCentre + vAvoid) - myPosition;
            moveDirection = Vector2.Lerp(moveDirection, direction.normalized, speed);
        }
    }
}