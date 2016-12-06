using UnityEngine;
using System.Collections;

public class ShadowingPhysics : MonoBehaviour {

    PlayerManager player;
    bool shadow;

	// Use this for initialization
	void Start () {
	   player = GameObject.Find("Player").GetComponent<PlayerManager>();
       shadow = false;
	}
	
	// Update is called once per frame
	void Update () {
       if (player.IsShadowing()) {
            GoShade();
       } else {
            GoNormal();
       }
	}

    void GoShade () {
        if (! shadow) {
            GetComponent<Collider>().isTrigger = true;
            shadow = true;
        }
    }

    void GoNormal () {
        if (shadow) {
            GetComponent<Collider>().isTrigger = false;
            shadow = false;
        }
    }

    void OnTriggerStay (Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<PlayerManager>().DetectWall();
        }
    }
}
