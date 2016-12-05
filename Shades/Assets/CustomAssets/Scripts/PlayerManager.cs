using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    bool shadowing;
    bool lighting;
    bool passingWall;
    // bool following;
    float recentDetection;
    float recentPassing;
    // Transform shadeFollowed;
    // Transform projectingLight;
    float burning;
    float cooldown;
    public Image screenColor;
    Color actualColor = Color.clear;
    Color color1 = new Color(1f, 0f, 0f, 0.1f);
    Color color2 = new Color(1f, 0f, 0f, 0.3f);
    Color color3 = new Color(1f, 0f, 0f, 0.4f);
    Color cooldownColor = new Color(1f, 0f, 1f, 0.05f);

	// Use this for initialization
	void Start () {
	   // shadowing = false;
    //    // following = false;
    //    var shape = GetComponent<ParticleSystem>().shape;
    //    shape.enabled = true;
    //    shape.shapeType = ParticleSystemShapeType.MeshRenderer;
    //    shape.meshShapeType = ParticleSystemMeshShapeType.Triangle;
    //    shape.meshRenderer = GetComponent<MeshRenderer>();
    //    GetComponent<ParticleSystem>().startSpeed = 0.1f;
    //    GetComponent<ParticleSystem>().startLifetime = 1f;
    //    GetComponent<ParticleSystem>().startColor = Color.black;
       burning = 0;
       cooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (lighting) {
    	   if (recentDetection > 0) {
                if (shadowing) {
                    burning += Time.deltaTime;
                    screenColor.color = Color.Lerp (screenColor.color, Color.clear, 1f * Time.deltaTime);
                    if (burning > 3) {
                        EjectFromShade();
                    } else if (burning > 2.5) {
                        ChangeColorFilter(color3);
                    } else if (burning > 1.5) {
                        ChangeColorFilter(color2);
                    } else if (burning > 0.5) {
                        ChangeColorFilter(color1);
                    }
                }
                recentDetection -= Time.deltaTime;
           } else {
                lighting = false;
                ChangeColorFilter(Color.clear);
           }
        } else if (burning > 0) {
            burning -= 2 * Time.deltaTime;
            if (burning < 0) {
                burning = 0;
            }
        }
        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
            screenColor.color = Color.Lerp(Color.clear, cooldownColor, cooldown);
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

    void ChangeColorFilter (Color color) {
        if (actualColor != color) {
            actualColor = color;
            screenColor.color = color;
        }
    }

    public void DetectLight () {
        lighting = true;
        recentDetection = Time.deltaTime;
    }

    public void ActualShade (Transform shade, Transform light) {
    //     if (shadeFollowed == null || Vector3.Distance(transform.position, light.position) <= Vector3.Distance(transform.position, projectingLight.position) || Vector3.Distance(transform.position, shade.position) <= Vector3.Distance(transform.position, shadeFollowed.position)) {
    //         shadeFollowed = shade;
    //         projectingLight = light;
    //     }
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

    void EjectFromShade () {
        ChangeColorFilter(Color.clear);
        shadowing = false;
        burning = 0;
        cooldown = 9;
        // GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        // var emission = GetComponent<ParticleSystem>().emission;
        // emission.enabled = true;
        // emission.rate = 0;
    }

    void SwitchForm () {
        if (shadowing && !passingWall) {
            ChangeColorFilter(Color.clear);
            shadowing = false;
            burning = 0;
            cooldown = 3;
            // GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            // var emission = GetComponent<ParticleSystem>().emission;
            // emission.enabled = true;
            // emission.rate = 0;
        } else if (! lighting && cooldown <= 0) {
            shadowing = true;
            // GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            // var emission = GetComponent<ParticleSystem>().emission;
            // emission.enabled = true;
            // emission.rate = 50;
        }
    }

    // void FollowShade () {
    //     if (shadowing) {
    //         following = true;
    //     }
    // }

    // void Unfollow () {
    //     following = false;
    // }
}
