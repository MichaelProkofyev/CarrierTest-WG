using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlaneController : MonoBehaviour {


    public float maxAngularSpeed = 130f;
    public float minSpeed = .2f, maxSpeed = 1f;
    public Transform previousPlaneT;
    public float followOffset = 1f;

    PlaneState state;
    Transform shipT, currentTargetT;
    float patrolDurationLeft = 30f;
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
                targetPos += (Vector3)Random.insideUnitCircle * followFuzziness + (transform.position - targetPos).normalized * followOffset;

                Debug.DrawLine(transform.position, targetPos, Color.blue);
                break;
            case PlaneState.RETURNING:
                targetPos = shipT.position;
                break;
            default:
                targetPos = Vector3.zero;
                break;
        }

        float currentSpeed = maxSpeed; //Max speed is the default speed

        bool isNearTarget = (transform.position - targetPos).sqrMagnitude <= followOffset * followOffset;
        if (isNearTarget)
        {
            switch (state)
            {
                case PlaneState.ON_PATROL:
                    //Descrease the speed if near target
                    currentSpeed = minSpeed;
                    break;
                case PlaneState.RETURNING:
                    PlanesManager.Instance.PlaneReturned(this);
                    return;
            }
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