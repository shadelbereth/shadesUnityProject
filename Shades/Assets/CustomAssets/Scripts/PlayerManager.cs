using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    bool shadowing;
    bool lighting;
    bool passingWall;
    bool following;
    float recentDetection;
    float recentPassing;
    Transform shadeFollowed;
    Transform projectingLight;

	// Use this for initialization
	void Start () {
	   shadowing = false;
       following = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (lighting) {
    	   if (recentDetection > 0) {
                print("burn");
                recentDetection -= Time.deltaTime;
           } else {
                    lighting = false;
           }
        }
        if (passingWall) {
           if (recentPassing > 0) {
                print("wall");
                recentPassing -= Time.deltaTime;
           } else {
                    passingWall = false;
           }
        }
        // if (following) {
        //     RaycastHit hit;
        //     if (Physics.Raycast(shadeFollowed.position, shadeFollowed.position - projectingLight.position, out hit, 10f - Vector3.Distance(projectingLight.position, shadeFollowed.position))) {
        //         if (transform.position != hit.point) {
        //             transform.position = Vector3.Lerp(transform.position, hit.point, 1f);
        //         }
        //     }
        // }
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            SwitchForm();
        }
        // if (Input.GetKeyDown(KeyCode.Q)) {
        //     if (following) {
        //         Unfollow();
        //     } else {
        //         FollowShade();
        //     }
        // }
	}

    public void DetectLight () {
        lighting = true;
        recentDetection = Time.deltaTime;
    }

    public void ActualShade (Transform shade, Transform light) {
        if (shadeFollowed == null || Vector3.Distance(transform.position, light.position) <= Vector3.Distance(transform.position, projectingLight.position) || Vector3.Distance(transform.position, shade.position) <= Vector3.Distance(transform.position, shadeFollowed.position)) {
            shadeFollowed = shade;
            projectingLight = light;
        }
    }

    public void DetectWall () {
        passingWall = true;
        recentPassing = Time.deltaTime;
    }

    public bool IsShadowing () {
        if (shadowing) {
            return true;
        }
        return false;
    }

    void SwitchForm () {
        if (shadowing && ! passingWall && ! following) {
            shadowing = false;
            // GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        } else if (! lighting) {
            shadowing = true;
            // GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }

    void FollowShade () {
        if (shadowing) {
            following = true;
        }
    }

    void Unfollow () {
        following = false;
    }
}
