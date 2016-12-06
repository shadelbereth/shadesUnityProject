using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraManager : MonoBehaviour {

    Transform player;
    // Transform lastWall;
    public float smoothing = 5f;
    Vector3 offset;
    BlurOptimized blur;
    SepiaTone sepia;

    // Use this for initialization
    void Start () {
       player = GameObject.Find("Player").transform;
       offset = transform.position - player.position;
       blur = GetComponent<BlurOptimized>();
       blur.enabled = false;
       sepia = GetComponent<SepiaTone>();
       sepia.enabled = false;
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

    public void Blur () {
        blur.enabled = true;
        StartCoroutine("Bluring");
    }

    public void Sepia (bool status) {
        if (sepia.enabled != status) {
            sepia.enabled = status;
        }
    }

    IEnumerator Bluring () {
        blur.blurIterations = 4;
        yield return new WaitForSeconds(0.25f);
        while (blur.blurIterations > 1) {
            blur.blurIterations--;
            yield return new WaitForSeconds(0.25f);
        }
        blur.enabled = false;
    }
}
