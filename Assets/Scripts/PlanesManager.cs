using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanesManager : SingletonComponent<PlanesManager> {

    public PlaneController planePrefab;
    public int AvailablePlanesNum
    {
        get { return availablePlanesNum; }
        set
        {
            availablePlanesNum = value;
            UIController.Instance.SetAvailablePlanesNumber(availablePlanesNum, maxAvailablePlanesNum);
        }
    }


    int availablePlanesNum = 5;
    int maxAvailablePlanesNum;
    Transform lastDeployedPlaneT;


    public void TrySpawnPlane ()
    {
        if (AvailablePlanesNum == 0) return;

        PlaneController planeClone = Instantiate(planePrefab, (Vector2)transform.position + Random.insideUnitCircle, Quaternion.identity) as PlaneController;
        planeClone.previousPlaneT = lastDeployedPlaneT;
        lastDeployedPlaneT = planeClone.transform;
        AvailablePlanesNum--;
    }

    public void PlaneReturned (PlaneController plane)
    {
        Destroy(plane.gameObject);
        AvailablePlanesNum++;
    }

    void Start ()
    {
        maxAvailablePlanesNum = availablePlanesNum;
        UIController.Instance.SetAvailablePlanesNumber(availablePlanesNum, maxAvailablePlanesNum);
    }
}
