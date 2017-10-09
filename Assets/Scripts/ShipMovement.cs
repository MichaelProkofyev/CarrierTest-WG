using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : SingletonComponent<ShipMovement> {
    
    float maxRotationForce = 1f;
    float maxAcceleration = 15f;


    float speedIncrement = 10f;
    float rotationIncrement = 2f;

    Rigidbody2D rb;
    float acceleration;
    float rotationForce;

    // Use this for initialization
    void Start () {
        rb = GetComponentInChildren<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {



        if (Input.GetButtonDown("SpeedUp"))
        {
            acceleration += speedIncrement;
            acceleration = Mathf.Clamp(acceleration, -maxAcceleration, maxAcceleration);

        }
        else if (Input.GetButtonDown("SpeedDown"))
        {
            acceleration -= speedIncrement;
            acceleration = Mathf.Clamp(acceleration, -maxAcceleration, maxAcceleration);

        }
        else if (Input.GetButtonDown("MakePlane"))
        {
            print("making plane");
        }

        if (Input.GetButton("TurnLeft"))
        {
            rotationForce += rotationIncrement * Time.deltaTime;
            rotationForce = Mathf.Clamp(rotationForce, -maxRotationForce, maxRotationForce);
        }
        else if (Input.GetButton("TurnRight"))
        {
            rotationForce -= rotationIncrement * Time.deltaTime;
            rotationForce = Mathf.Clamp(rotationForce, -maxRotationForce, maxRotationForce);

        }

    }

    void FixedUpdate() 
    {
        rb.AddRelativeForce(Vector2.up * acceleration * Time.fixedDeltaTime, ForceMode2D.Impulse);
        //rb.AddTorque(rotationForce * rb.velocity.magnitude, ForceMode2D.Impulse);
        rb.MoveRotation(rb.rotation + rotationForce * rb.velocity.magnitude*.5f);

    }

    void OnGUI()
    {
        GUI.Box(new Rect(5 , 5, 200, 25), "velocity " + rb.velocity);
        GUI.Box(new Rect(5, 30, 200, 25), "acceleration " + acceleration);
        GUI.Box(new Rect(5, 55, 200, 25), "rotation force " + rotationForce);


    }
}
