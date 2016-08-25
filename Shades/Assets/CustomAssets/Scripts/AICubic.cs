using UnityEngine;
using System.Collections;

public class AICubic : MonoBehaviour {

    NavMeshAgent agent;
    public Transform wpGroup;
    Transform[] wps;
    Transform lastTarget;

	// Use this for initialization
	void Start () {
       agent = GetComponent<NavMeshAgent>();
       wps = wpGroup.GetComponentsInChildren<Transform>();
       lastTarget = SelectDestination();
       agent.SetDestination(lastTarget.position);
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
       agent.SetDestination(destination.position);
            lastTarget = destination;
        }
    } 

    Transform SelectDestination () {
        return wps[Random.Range(1, wps.Length)];
    }
}
