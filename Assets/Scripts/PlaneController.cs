using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {

    public float maxAngularVelocity = 60;
     
    Transform target;

    float maxVelocity = 1f;

    Vector2 velocity = Vector2.zero;

	// Use this for initialization
	void Start () {
        target = ShipMovement.Instance.transform;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 desired_velocity = (target.position - transform.position).normalized * maxVelocity;
        Vector2 steering = (desired_velocity - velocity).normalized * (maxAngularVelocity * Time.deltaTime);
        //print(Vector2.Angle(desired_velocity, velocity));


        velocity += steering;
        transform.position += (Vector3)velocity * Time.deltaTime;

        //Rotate to face target
        Vector2 frameTarget = velocity - (Vector2)transform.position; //?????
        float rot_z = Mathf.Atan2(desired_velocity.y, desired_velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}