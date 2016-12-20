using UnityEngine;
using System.Collections;

public class AutomaticDoor : MonoBehaviour {

    Animator anim;
    float detectTime;

	// Use this for initialization
	void Start () {
	   anim = GetComponentInChildren<Animator>();
       detectTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
	   if (detectTime > 0) {
            detectTime -= Time.deltaTime;
       } else {
            anim.SetBool("opened", false);
       }
	}

    void OnTriggerStay (Collider other) {
        if (other.tag == "Ennemy" || (other.tag == "Player" && other.GetComponent<PlayerManager>().HavePasskey())) {
            anim.SetBool("opened", true);
            detectTime = 1f;
        }
    }

    void OnTriggerEnter (Collider other) {
        if (other.tag == "Ennemy" || (other.tag == "Player" && other.GetComponent<PlayerManager>().HavePasskey())) {
            anim.SetBool("opened", true);
            detectTime = 1f;
        }
    }
}
