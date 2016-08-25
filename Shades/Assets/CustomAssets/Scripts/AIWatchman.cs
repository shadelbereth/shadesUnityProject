using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class AIWatchman : MonoBehaviour {

    AICharacterControl controller;
    public Transform wpGroup;
    Transform[] wps;
    Transform lastTarget;

	// Use this for initialization
	void Start () {
       controller = GetComponent<AICharacterControl>();
	   wps = wpGroup.GetComponentsInChildren<Transform>();
       controller.target = SelectDestination();
       lastTarget = controller.target;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter (Collider other) {
        if (other.transform == lastTarget) {
            Transform destination = SelectDestination();
            while (destination == lastTarget) {
                destination = SelectDestination();
            }
            controller.target = destination;
            lastTarget = destination;
        }
    } 

    Transform SelectDestination () {
        return wps[Random.Range(1, wps.Length)];
    }
}
