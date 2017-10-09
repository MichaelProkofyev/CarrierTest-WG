using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : SingletonComponent<SingletonComponent> {
    
    int maxSpeed = 15;
    int minSpeed = -5;
    int speedIncrement = 5;
    int speed = 0;

    public int rotationSpeed;

    PlanesManager planesManager;

    // Use this for initialization
    void Start () {
        planesManager = GetComponent<PlanesManager>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("SpeedUp"))
        {
            speed += speedIncrement;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

        }
        else if (Input.GetButtonDown("SpeedDown"))
        {
            speed -= speedIncrement;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
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
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
