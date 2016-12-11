using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    public Text tutorialText;
    public bool moveTutorial = true;
    bool waitingForCapture;
    public bool hideTutorial = true;
    bool waitingForReject;
    public bool lightRejectTutorial = true;
    bool waitingForCooldown;
    public bool cooldownTutoriel = true;
    string moveText;
    string hideText;
    string lightRejectText;
    string cooldownText;

    public float fadingDelay = 1f;
    PlayerManager player;
    float fadingTime;
    bool fading;

	// Use this for initialization
	void Start () {
       player = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
       fading = false;
       fadingTime = fadingDelay;
       waitingForCapture = hideTutorial;
       waitingForReject = lightRejectTutorial;
       waitingForCooldown = cooldownTutoriel;
       moveText = "TO MOVE, KEEP MOUSE LEFT BUTTON DOWN";
       hideText = "THEY'RE CAPTURING YOU! RUN! TO TURN SHADE, GO HIDE IN THE SHADE THEN PRESS SPACE.";
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
            if (player.IsShadowing()) {
                fading = true;
            }
            if (fading) {
                hideTutorial = FadeAfterDelay();
            }
       } else if (waitingForReject) {
            tutorialText.text = "";
            waitingForReject = !player.IsBurning();
       } else if (lightRejectTutorial) {
            tutorialText.text = lightRejectText;
            if (!player.IsBurning()) {
                fading = true;
            }
            if (fading) {
                lightRejectTutorial = FadeAfterDelay();
            }
       } else if (waitingForCooldown) {
            tutorialText.text = "";
            waitingForCooldown = !player.IsInCooldown();
       } else if (cooldownTutoriel) {
            tutorialText.text = cooldownText;
            if (!player.IsInCooldown()) {
                fading = true;
            }
            if (fading) {
                cooldownTutoriel = FadeAfterDelay();
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
        fadingTime = fadingDelay;
        fading = false;
        return false;
    }
}
