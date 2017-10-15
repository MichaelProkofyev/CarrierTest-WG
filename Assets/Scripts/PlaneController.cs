using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlaneController : MonoBehaviour {


    public float maxAngularSpeed = 130f;
    public float minSpeed = .2f, maxSpeed = 1f;
    public Transform previousPlaneT;
    public float followOffset = 1f;
    public float landingDistance = .5f;

    PlaneState state;
    Transform shipT;
    float patrolDurationLeft = 20f;
    float followFuzziness = .01f;
    

    enum PlaneState
    {
        ON_PATROL,
        RETURNING
    }

    void Start ()
    {
        shipT = ShipController.Instance.transform;
    }

    void Update()
    {
        Vector3 targetPos;
        float currentSpeed = maxSpeed; //Max speed is the default speed
        float distanceToShip = Vector3.Magnitude(shipT.position - transform.position);

        //Choose target
        switch (state)
        {
            case PlaneState.ON_PATROL:
                patrolDurationLeft -= Time.deltaTime;
                if (patrolDurationLeft <= 0)
                {
                    state = PlaneState.RETURNING;
                    goto case PlaneState.RETURNING; //questionable
                }
                //If no plane to follow, follow the ship
                targetPos = previousPlaneT ? previousPlaneT.position : shipT.position;

                //Offset from target
                targetPos += (transform.position - targetPos).normalized * followOffset;

                if (previousPlaneT)
                {
                    bool tooCloseToShip = distanceToShip < followOffset;
                    if (tooCloseToShip)
                    {
                        //Avoid ship when following other plane
                        targetPos +=  (targetPos - shipT.position).normalized * (followOffset - distanceToShip);
                    }
                }


                //Add a bit of randomness for more interesting follow behaviour
                targetPos += (Vector3)Random.insideUnitCircle * followFuzziness;

                Debug.DrawLine(transform.position, targetPos, Color.blue);

                bool isNearTarget = (transform.position - targetPos).sqrMagnitude <= followOffset * followOffset;
                if (isNearTarget)
                {
                    //Descrease the speed if near target
                    currentSpeed = minSpeed;
                }

                break;
            case PlaneState.RETURNING:
                targetPos = shipT.position;
                bool canLand = distanceToShip <= landingDistance;
                if (canLand)
                {
                    //Descrease the speed near target for better chance to land
                    currentSpeed = minSpeed;
                    PlanesManager.Instance.PlaneReturned(this);
                    return;
                }
                if (distanceToShip < landingDistance * 1.5f)
                {
                    currentSpeed = minSpeed;
                }
                break;
            default:
                targetPos = Vector3.zero;
                break;
        }

        //Get steering vector
        Vector2 desiredVelocity = (targetPos - transform.position).normalized * currentSpeed;
        float maxRotationStep = maxAngularSpeed * Time.deltaTime;
        Vector2 steering = Vector3.RotateTowards(transform.up, desiredVelocity, maxRotationStep * Mathf.Deg2Rad, 0);

        Debug.DrawRay(transform.position, steering, Color.red);

        //Rotate
        float rotationAngle = Mathf.Atan2(steering.y, steering.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotationAngle - 90f, Vector3.forward);

        //Update position
        transform.position += transform.up * currentSpeed * Time.deltaTime;
    }
}