using UnityEngine;
using System.Collections;

public class HidingManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PasskeyHiding[] hidings = GetComponentsInChildren<PasskeyHiding>();
		bool oneAtLeast = false;
		while (!oneAtLeast) {
			foreach (PasskeyHiding hiding in hidings) {
				if (hiding.GenerateKey(hidings.Length)) {
					oneAtLeast = true;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
