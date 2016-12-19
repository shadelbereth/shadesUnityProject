using UnityEngine;
using System.Collections;

public class DetectPlayerInZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit (Collider other) {
		if (other.tag == "Player") {
			other.GetComponent<PlayerManager>().DetectImprisoning(false);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			other.GetComponent<PlayerManager>().DetectImprisoning(true);
		}
	}
}
