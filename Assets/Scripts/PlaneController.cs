using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlaneState
{
    ON_PATROL,
    RETURNING
}

public class PlaneController : MonoBehaviour {

    public float angularVelocity = 15;
     
    Transform shipT;

    public float speed = 1f;
    Vector2 velocity = Vector2.zero;



	// Use this for initialization
	void Start () {
        shipT = ShipController.Instance.transform;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.up, Color.green); //Current Velocity ray

        Vector2 desired_velocity = (shipT.position - transform.position).normalized * speed;

        float rotationStep = angularVelocity * Mathf.Deg2Rad * Time.deltaTime;
        Vector2 steering = Vector3.RotateTowards(transform.up, desired_velocity, rotationStep, 0);

        float angle = Mathf.Atan2(steering.y, steering.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        //Move in the chosen direction
        transform.position += transform.up * speed * Time.deltaTime;



        Debug.DrawRay(transform.position, steering, Color.red); //Seek ray
    }

    // Update is called once per frame
    void Update2 () {
        Vector2 desired_velocity = (shipT.position - transform.position).normalized * speed;

        Vector2 steering = (desired_velocity - velocity).normalized * (angularVelocity * Time.deltaTime);
        //print(Vector2.Angle(desired_velocity, velocity));

        velocity += steering;
        transform.position += (Vector3)velocity * Time.deltaTime;

        //Rotate to face target
       // Vector2 frameTarget = velocity - (Vector2)transform.position; //NOt used, how to face seek velocity?
        float rot_z = Mathf.Atan2(desired_velocity.y, desired_velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}