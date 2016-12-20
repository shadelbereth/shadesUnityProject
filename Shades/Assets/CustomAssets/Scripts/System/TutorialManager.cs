using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    public Text tutorialText;
    public bool moveTutorial = true;
    bool moveTutorial2;
    public bool escapeTutorial = true;
    bool escapeTutorial2;
    bool escapeTutorial3;
    bool waitingForCapture;
    public bool hideTutorial = true;
    bool waitingForReject;
    public bool lightRejectTutorial = true;
    bool waitingForCooldown;
    public bool cooldownTutoriel = true;
    bool keypassFound;
    bool nothingFound;
    string moveText;
    string moveText2;
    string escapeText;
    string escapeText2;
    string escapeText3;
    string hideText;
    string lightRejectText;
    string cooldownText;
    string keypassFoundText;
    string nothingFoundText;

    public float fadingDelay = 1f;
    PlayerManager player;
    float fadingTime;
    bool fading;

	// Use this for initialization
	void Start () {
       player = GameObject.Find("Player").GetComponent<PlayerManager>();
       fading = false;
       fadingTime = fadingDelay * 3;
       moveTutorial2 = moveTutorial;
       escapeTutorial2 = escapeTutorial;
       escapeTutorial3 = escapeTutorial;
       waitingForCapture = hideTutorial;
       waitingForReject = lightRejectTutorial;
       waitingForCooldown = cooldownTutoriel;
       moveText = "WHERE AM I? I DON'T RECOGNIZE THIS PLACE.";
       moveText2 = "[ TO MOVE KEEP LEFT MOUSE BUTTON DOWN ]";
       escapeText = "IT LOOKS LIKE A PRISON. I COULD TRY TO BYPASS GUARDIANS THE NEXT TIME THEY COME IN...";
       escapeText2 = "... OR I COULD TRICK THEM WITH MY SPECIAL GIFT.";
       escapeText3 = "[ TO TURN SHADE PRESS SPACE WHILE HIDING IN THE DARK ]";
       lightRejectText = "IT HURTS! THE LIGHT HURTS!";
       cooldownText = "TURNING SHADE IS EXHAUSTING, IT TAKES TIME TO DO IT AGAIN. ESPECIALLY WHEN THE LIGHT HAS EJECTED ME.";
       hideText = "NO, THEY WON'T CAPTURE ME AGAIN! I MUST RUN INTO THE SHADE.";
       keypassFoundText = "OH! THERE IS A KEYPASS HERE, I'LL HAVE NO MORE PROBLEM TO OPEN DOORS.";
       nothingFoundText = "THERE IS NOTHING OF INTEREST HERE.";
	}
	
	// Update is called once per frame
	void Update () {
       if (moveTutorial) {
            tutorialText.text = moveText;
            moveTutorial = FadeAfterDelay();
       } else if (moveTutorial2) {
            tutorialText.text = moveText2;
            if (Input.GetMouseButton(0)) {
                fading = true;
            }
            if (fading) {
                moveTutorial2 = FadeAfterDelay(true);
            }
       } else if (escapeTutorial) {
            tutorialText.text = escapeText;
            escapeTutorial = FadeAfterDelay(true);
       } else if (escapeTutorial2) {
            tutorialText.text = escapeText2;
            escapeTutorial2 = FadeAfterDelay();
       } else if (escapeTutorial3) {
            tutorialText.text = escapeText3;
            if (player.IsShadowing()) {
                fading = true;
            }
            if (fading) {
                escapeTutorial3 = FadeAfterDelay();
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
                cooldownTutoriel = FadeAfterDelay(true);
            }
       } else if (waitingForCapture) {
            tutorialText.text = "";
            waitingForCapture = !player.IsBeingCaptured();
       } else if (hideTutorial) {
            tutorialText.text = hideText;
            hideTutorial = FadeAfterDelay();
       } else {
            tutorialText.text = "";
       }
       if (nothingFound) {
        tutorialText.text = nothingFoundText;
        nothingFound = FadeAfterDelay();
       }
       if (keypassFound) {
        tutorialText.text = keypassFoundText;
        keypassFound = FadeAfterDelay();
       }
	}

  bool FadeAfterDelay (bool longDelayNext = false) {
      if (fadingTime > 0) {
          fadingTime -= Time.deltaTime;
          return true;
      }
      fadingTime = fadingDelay;
      if (longDelayNext) {
          fadingTime *= 3;
      }
      fading = false;
      return false;
  }

  public void SnoopingResult (bool result) {
    fadingTime *= 3;
    if (result) {
      keypassFound = true;
    } else {
      nothingFound = true;
    }
  }
}
