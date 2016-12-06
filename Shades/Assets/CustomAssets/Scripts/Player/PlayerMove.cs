using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerMove : MonoBehaviour {

  public float speed = 6f;
  bool alive;
  Rigidbody playerRigidbody;
  PlayerManager manager;
  ThirdPersonCharacter thirdPersonCharacter;

  // private NavMeshAgent agent;

  // Use this for initialization
  void Start () {
     // agent = GetComponent<NavMeshAgent>();
    playerRigidbody = GetComponent<Rigidbody>();
    manager = GetComponent<PlayerManager>();
    thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
    alive = true;
  }
  
  // Update is called once per frame
  void FixedUpdate () {
     if (alive) {
       bool crouch;
       if (manager.IsShadowing()) {
          crouch = true;
          thirdPersonCharacter.m_MoveSpeedMultiplier = 1.7f;
          thirdPersonCharacter.m_AnimSpeedMultiplier = 1.7f;
       } else {
          crouch = false;
          thirdPersonCharacter.m_MoveSpeedMultiplier = 1f;
          thirdPersonCharacter.m_AnimSpeedMultiplier = 1f;
       }
       if (Input.GetMouseButton(0)) {
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          RaycastHit hit;
          Physics.Raycast(ray, out hit);
          // agent.destination = hit.point;


          Vector3 playerDirection = hit.point - transform.position;
          if (thirdPersonCharacter == null) {
            playerDirection.y = 0f;
            playerDirection = playerDirection.normalized * speed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + playerDirection);
          } else {
            playerDirection.y = transform.position.y;
            playerDirection = playerDirection.normalized;
            thirdPersonCharacter.Move(playerDirection, crouch, false);
          }
          // transform.position = transform.position + playerDirection;
       } else {
          thirdPersonCharacter.Move(Vector3.zero, crouch, false);
       }
    } else {
      thirdPersonCharacter.Move(Vector3.zero, true, false);
      Camera.main.GetComponent<CameraManager>().Blur();
    }
  }

  public void Fall () {
    alive = false;
    playerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
  }
}
