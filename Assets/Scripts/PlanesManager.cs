using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanesManager : MonoBehaviour {

    public PlaneController planePrefab;

    int availablePlanes = 5;


    public void TrySpawnPlane ()
    {
        if (availablePlanes == 0) return;

        Instantiate(planePrefab, (Vector2)transform.position + Random.insideUnitCircle, Quaternion.identity);
        availablePlanes--;
    }

    void PlaneReturned (PlaneController plane)
    {
        Destroy(plane.gameObject);
        availablePlanes++;
    }
     
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
