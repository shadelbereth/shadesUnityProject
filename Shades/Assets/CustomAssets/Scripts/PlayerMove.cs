using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

  public float speed = 6f;
  Rigidbody playerRigidbody;

  // private NavMeshAgent agent;

  // Use this for initialization
  void Start () {
     // agent = GetComponent<NavMeshAgent>();
    playerRigidbody = GetComponent<Rigidbody>();
  }
  
  // Update is called once per frame
  void Update () {
     if (Input.GetMouseButton(0)) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        // agent.destination = hit.point;

        Vector3 playerDirection = hit.point - transform.position;
        playerDirection = playerDirection.normalized * speed * Time.deltaTime;
        playerDirection.y = 0f;
        playerRigidbody.MovePosition(transform.position + playerDirection);
        // transform.position = transform.position + playerDirection;
     }
  }
}
