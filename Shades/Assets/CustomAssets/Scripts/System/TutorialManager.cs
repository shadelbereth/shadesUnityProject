using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    public Text tutorialText;
    public bool moveTutorial = true;
    bool waitingForCapture;
    public bool hideTutorial = true;
    public bool turnShadeTutorial = true;
    bool waitingForReject;
    public bool lightRejectTutorial = true;
    bool waitingForCooldown;
    public bool cooldownTutoriel = true;
    string moveText;
    string hideText;
    string turnShadeText;
    string lightRejectText;
    string cooldownText;

    PlayerManager player;
    float fadingTime = 2f;
    bool fading;

	// Use this for initialization
	void Start () {
       player = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
       if (moveTutorial) {
            tutorialText.text = moveText;
       } else if (turnShadeTutorial) {
            tutorialText.text = turnShadeText;
       }
       fading = false;
       waitingForCapture = hideTutorial;
       waitingForReject = lightRejectTutorial;
       waitingForCooldown = cooldownTutoriel;
       moveText = "KEEP MOUSE LEFT BUTTON DOWN TO MOVE";
       hideText = "THEY'RE CAPTURING YOU! RUN, GO HIDE IN THE SHADE";
       turnShadeText = "THEN PRESS SPACE TO TURN SHADE";
       lightRejectText = "IT HURTS! THE LIGHT HURTS!";
       cooldownText = "TURNING SHADE IS EXHAUSTING, IT TAKES TIME TO DO IT AGAIN. ESPECIALLY YOU DIDN'T CHOSE TO QUIT SHADES";
	}
	
	// Update is called once per frame
	void Update () {
       if (moveTutorial) {
            tutorialText.text = moveText;
            if (Input.GetMouseButton(0)) {
                fading = true;
            }
            if (fading) {
                moveTutorial = FadeAfterDelay();
            }
       } else if (waitingForCapture) {
            tutorialText.text = "";
            waitingForCapture = !player.IsBeingCaptured();
       } else if (hideTutorial) {
            tutorialText.text = hideText;
            if (!player.IsInTheLight()) {
                fading = true;
            }
            if (fading) {
                hideTutorial = FadeAfterDelay();
            }
       } else if (turnShadeTutorial) {
            tutorialText.text = turnShadeText;
            if (player.IsShadowing()) {
                fading = true;
            }
            if (fading) {
                turnShadeTutorial = FadeAfterDelay();
            }
       } else if (waitingForReject) {
            tutorialText.text = "";
            waitingForReject = !player.IsBurning();
       } else if (lightRejectTutorial) {
            tutorialText.text = lightRejectText;
            if (player.IsShadowing()) {
                fading = true;
            }
            if (fading) {
                lightRejectTutorial = FadeAfterDelay();
            }
       } else if (waitingForCooldown) {
            tutorialText.text = "";
            waitingForReject = !player.IsInCooldown();
       } else if (cooldownTutoriel) {
            tutorialText.text = cooldownText;
            fading = true;
            if (fading) {
                lightRejectTutorial = FadeAfterDelay();
            }
       } else {
            tutorialText.text = "";
       }
	}

    bool FadeAfterDelay () {
        if (fadingTime > 0) {
            fadingTime -= Time.deltaTime;
            return true;
        }
        fadingTime = 1f;
        fading = false;
        return false;
    }
}
