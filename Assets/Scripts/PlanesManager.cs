using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanesManager : SingletonComponent<PlanesManager> {

    public PlaneController planePrefab;

    int availablePlanes = 5;

    Transform lastDeployedPlaneT;

    public void TrySpawnPlane ()
    {
        if (availablePlanes == 0) return;

        PlaneController planeClone = Instantiate(planePrefab, (Vector2)transform.position + Random.insideUnitCircle, Quaternion.identity) as PlaneController;
        planeClone.previousPlaneT = lastDeployedPlaneT;
        lastDeployedPlaneT = planeClone.transform;
        availablePlanes--;
    }

    public void PlaneReturned (PlaneController plane)
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
