using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class LifeOfPlayer : MonoBehaviour {

    public int life = 3;
    public string name = "pierre";
    
    [System.Serializable]
    public class OnPlayerIsDead : UnityEvent<string> {

    }
    public OnPlayerIsDead onPlayerIsDead;



	// Use this for initialization
	void OnValidate () {
	   if (life <= 0) {
            print("dead");
            onPlayerIsDead.Invoke(name);
       }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
