using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : SingletonComponent<UIController> {

    [SerializeField]
    Text planesAvailableText;
    
    public void SetAvailablePlanesNumber(int planes, int maxPlanes)
    {
        planesAvailableText.text = string.Format("Planes Available: {0}/{1}", planes, maxPlanes);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
