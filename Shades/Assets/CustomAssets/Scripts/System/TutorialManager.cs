using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    public Text tutorialText;
    public bool turnShadeTutorial;
    string turnShadeText;
    PlayerManager player;

	// Use this for initialization
	void Start () {
	   if (turnShadeTutorial) {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
            turnShadeText = "PRESS SPACE IN A DARK PLACE";
            tutorialText.text = turnShadeText;
       }
	}
	
	// Update is called once per frame
	void Update () {
	   if (turnShadeTutorial) {
            if (player.IsShadowing()) {
                turnShadeTutorial = false;
                tutorialText.text = "";
            }
       }
	}
}
