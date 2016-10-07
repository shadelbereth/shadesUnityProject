using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    Transform player;
    Transform lastWall;
    public float smoothing = 5f;
    Vector3 offset;

    // Use this for initialization
    void Start () {
       player = GameObject.Find("Player").transform;
       offset = transform.position - player.position;
	}
	
	// Update is called once per frame
	void Update () {
	   // RaycastHit hit;
    //     if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, 10f)) {
    //         if (hit.collider.tag == "ExternWall") {
    //             if (lastWall == null || hit.collider.transform != lastWall) {
    //                 hit.collider.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
    //                 if (lastWall != null) {
    //                     lastWall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    //                 }
    //                 lastWall = hit.collider.transform;
    //             }
    //         }
    //     }
	}

    void FixedUpdate () {
       Vector3 targetCamPos = player.position + offset;
       transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
