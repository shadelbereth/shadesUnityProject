using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class AIWatchman : MonoBehaviour {

    AICharacterControl controller;
    ThirdPersonCharacter character;
    public Transform wpGroup;
    public float losingTiming = 3f;
    Transform[] wps;
    Transform lastTarget;
    Transform player;
    Transform lastKnownPosition;
    bool chasing;
    bool losing;
    float lookingFor;
    float hiting;

	// Use this for initialization
	void Start () {
       controller = GetComponent<AICharacterControl>();
       character = GetComponent<ThirdPersonCharacter>();
       player = GameObject.Find("Player").transform;
       lastKnownPosition = new GameObject().transform;
       lastKnownPosition.gameObject.AddComponent<BoxCollider>();
       lastKnownPosition.GetComponent<BoxCollider>().size = new Vector3(1f,1f,1f);
       lastKnownPosition.GetComponent<Collider>().isTrigger = true;
       lastKnownPosition.position = player.position;
       lastKnownPosition.parent = wpGroup;
	   wps = wpGroup.GetComponentsInChildren<Transform>();
       controller.target = SelectDestination();
       lastTarget = controller.target;
       chasing = false;
       losing = false;
       lookingFor = 0;
       hiting = 0;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Vector3 facing = transform.TransformDirection(Vector3.forward);
        chasing = false;
        if (Physics.Raycast(transform.position, player.position - transform.position, out hit)) {
            if (player == hit.collider.transform && Vector3.Angle(facing, player.position - transform.position) <= 180 && !player.GetComponent<PlayerManager>().IsShadowing()) {
                chasing = true;
                losing = false;
            }
        }
        if (chasing) {
            controller.target = player;
            character.m_MoveSpeedMultiplier = 2f;
            character.m_AnimSpeedMultiplier = 2f;
        } else {
            if (controller.target == player) {
                losing = true;
                lookingFor = losingTiming;
                character.m_MoveSpeedMultiplier = Mathf.Lerp(2f, 1f, losingTiming);
                character.m_AnimSpeedMultiplier = Mathf.Lerp(2f, 1f, losingTiming);
                lastKnownPosition.position = player.position;
                controller.target = lastKnownPosition;
            }
            if (losing) {
                lookingFor -= Time.deltaTime;
                if (lookingFor <= 0) {
                    losing = false;
                    Transform destination = SelectDestination();
                    while (destination == lastKnownPosition) {
                        destination = SelectDestination();
                    }
                    controller.target = destination;
                    lastTarget = destination;
                }
            }
        }
        if (hiting > 0) {
            hiting -= Time.deltaTime;
        }
	}

    void OnTriggerEnter (Collider other) {
        if (other.transform == lastTarget) {
            Transform destination = SelectDestination();
            while (destination == lastTarget) {
                destination = SelectDestination();
            }
            controller.target = destination;
            lastTarget = destination;
        }
    } 

    void OnCollisionEnter (Collision other) {
        if (other.gameObject.tag == "Player") {
            if (hiting <= 0) {
                hiting = 1f;
                player.GetComponent<PlayerManager>().Hurt();
            }
        }
    }

    Transform SelectDestination () {
        return wps[Random.Range(1, wps.Length)];
    }
}
