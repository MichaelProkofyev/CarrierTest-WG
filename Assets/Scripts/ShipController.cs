using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : SingletonComponent<SingletonComponent> {
    
    public int minSpeed = -5, maxSpeed = 15, speedIncrement = 5;
    public float rotationSpeed = 10f;

    int currentSpeed = 0;


    PlanesManager planesManager;

    // Use this for initialization
    void Start () {
        planesManager = GetComponent<PlanesManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("SpeedUp"))
        {
            currentSpeed += speedIncrement;
            currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);

        }
        else if (Input.GetButtonDown("SpeedDown"))
        {
            currentSpeed -= speedIncrement;
            currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
        }
        else if (Input.GetButtonDown("MakePlane"))
        {
            planesManager.TrySpawnPlane();
        }

        float rotation = 0;
        if (Input.GetButton("TurnLeft"))
        {
            rotation += rotationSpeed;
        }
        else if (Input.GetButton("TurnRight"))
        {
            rotation -= rotationSpeed;
        }

        transform.Rotate(0, 0, rotation);
        transform.Translate(Vector2.up * currentSpeed * Time.deltaTime);
    }
}
