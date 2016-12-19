using UnityEngine;
using System.Collections;

public class LightOrShadeDetection : MonoBehaviour {

    Transform player;

	// Use this for initialization
	void Start () {
	   player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, player.position - transform.position, out hit, 10f)) {
            if (player == hit.collider.transform) {
                player.GetComponent<PlayerManager>().DetectLight();
            } else {
                // Transform lastHit = hit.collider.transform;
                // float distance = Vector3.Distance(transform.position, lastHit.position);
                // if (Physics.Raycast(lastHit.position, player.transform.position - lastHit.position, out hit, 10f - distance) && player.transform == hit.collider.transform) {
                //     player.GetComponent<PlayerManager>().ActualShade(lastHit, transform);
                // }
            }
        }
	}

    // void OnTriggerStay(Collider other) {
    //     if (other.tag == "Player") {
    //         player = other.transform;
    //         RaycastHit hit;
    //         if (Physics.Raycast(transform.position, other.transform.position - transform.position, out hit, 20f) && other.transform == hit.collider.transform) {
    //             print("burn");
    //         }
    //     }
    // }
}
