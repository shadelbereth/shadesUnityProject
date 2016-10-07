using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    private NavMeshAgent agent;

    // Use this for initialization
    void Start () {
       agent = GetComponent<NavMeshAgent>();
    }
    
    // Update is called once per frame
    void Update () {
       if (Input.GetMouseButton(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            agent.destination = hit.point;
       }
    }
}
