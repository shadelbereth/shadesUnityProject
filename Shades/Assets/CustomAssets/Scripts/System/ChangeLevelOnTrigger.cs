using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeLevelOnTrigger : MonoBehaviour {
    public string nextLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter (Collider other) {
        if (other.tag == "Player") {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
