using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour {

    private int random;

    // Use this for initialization
    void Start () {        
    }
	
	// Update is called once per frame
	void Update () {
        random = Random.Range(0, 4);

        Debug.Log(random);
        if (random == 0)
        {
            transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            Debug.Log("LIGHT ON");
        }
        if (random == 1 || random == 2)
        {
            transform.position = new Vector3(0.0f, 100.0f, 0.0f);
            Debug.Log("LIGHT OFF");
        }
    }
}
