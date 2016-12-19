using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class AIWatchman : MonoBehaviour {

    AICharacterControl controller;
    ThirdPersonCharacter character;
    public Transform wpGroup;
    public float losingTiming = 2f;
    Transform[] wps;
    Transform lastTarget;
    Transform player;
    Transform lastKnownPosition;
    bool chasing;
    bool losing;
    bool lastSeen;
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
       lastKnownPosition.parent = wpGroup;
	   wps = wpGroup.GetComponentsInChildren<Transform>();
       lastKnownPosition.position = new Vector3(player.position.x, wps[1].position.y, player.position.z);
       controller.target = SelectDestination();
       lastTarget = controller.target;
       chasing = false;
       losing = false;
       lastSeen = false;
       lookingFor = 0;
       hiting = 0;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Vector3 facing = transform.TransformDirection(Vector3.forward);
        chasing = false;
        if (Physics.Raycast(transform.position, player.position - transform.position, out hit)) {
            if (player == hit.collider.transform && Vector3.Angle(facing, player.position - transform.position) <= 180 && !player.GetComponent<PlayerManager>().IsShadowing() && !player.GetComponent<PlayerManager>().IsImprisoned()) {
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
                character.m_MoveSpeedMultiplier = Mathf.Lerp(2f, 1f, losingTiming);
                character.m_AnimSpeedMultiplier = Mathf.Lerp(2f, 1f, losingTiming);
                lastKnownPosition.position = new Vector3(player.position.x, lastKnownPosition.position.y, player.position.z);
                controller.target = lastKnownPosition;
            }
            if (lastSeen) {
                StartCoroutine("LookingFor");
                lookingFor = losingTiming;
                lastSeen = false;
            }
            if (lookingFor > 0 && losing) {
                lookingFor -= Time.deltaTime;
                if (lookingFor > losingTiming * 0.75f) {
                    character.Move(Vector3.left, false, false);
                } else if (lookingFor > losingTiming * 0.5f) {
                    character.Move(Vector3.back, false, false);
                }else if (lookingFor > losingTiming * 0.25f) {
                    character.Move(Vector3.right, false, false);
                } else {
                    character.Move(Vector3.forward, false, false);
                }
            }
        }
        if (hiting > 0) {
            hiting -= Time.deltaTime;
        }
	}

    IEnumerator LookingFor() {
        yield return new WaitForSeconds(losingTiming);
        losing = false;
        Transform destination = SelectDestination();
        while (destination == lastKnownPosition) {
            destination = SelectDestination();
        }
        controller.target = destination;
        lastTarget = destination;
    }

    void OnTriggerStay (Collider other) {
        if (losing) {
            if (other.transform == lastKnownPosition && lookingFor <= 0) {
                lastSeen = true;
            }
        }
    }

    void OnTriggerEnter (Collider other) {
        if (losing) {
            if (other.transform == lastKnownPosition) {
                lastSeen = true;
            }
        } else if (other.transform == lastTarget) {
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

    void OnCollisionStay (Collision other) {
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
