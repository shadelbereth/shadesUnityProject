using UnityEngine;
using System.Collections;

public class PasskeyHiding : MonoBehaviour {

	bool hideKey;
	PlayerManager player;
	TutorialManager tutorial;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<PlayerManager>();
		tutorial = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool GenerateKey (int hidingCount) {
		if (Random.Range(1, hidingCount) == 1) {
			hideKey = true;
		} else {
			hideKey = false;
		}
		return hideKey;
	}

	void OnMouseEnter () {
		if (!player.IsShadowing()) {
			GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", new Color(0.7f, 0.7f,0.7f));
		}
	}

	void OnMouseExit () {
		if (!player.IsShadowing()) {
			GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.black);
		}
	}

	void OnMouseUp () {
		if (!player.IsShadowing()) {
			if (hideKey) {
				player.GainPasskey();
				hideKey = false;
				tutorial.SnoopingResult(true);
			} else {
				tutorial.SnoopingResult(false);
			}
		}
	}
}
