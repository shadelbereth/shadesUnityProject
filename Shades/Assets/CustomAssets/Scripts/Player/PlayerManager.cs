using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    bool shadowing;
    bool lighting;
    bool passingWall;
    bool alive;
    // bool following;
    float recentDetection;
    float recentPassing;
    // Transform shadeFollowed;
    // Transform projectingLight;
    float burning;
    float cooldown;
    public float cooldownMax = 9;
    float capturing;
    public Image screenColor;
    public Slider capturingBar;
    public Image ectoIcon;
    public string defeatScene;
    Color actualColor = Color.clear;
    Color color1 = new Color(1f, 0f, 0f, 0.1f);
    Color color2 = new Color(1f, 0f, 0f, 0.3f);
    Color color3 = new Color(1f, 0f, 0f, 0.4f);
    Color cooldownColor = new Color(1f, 0f, 1f, 0.1f);
    CameraManager cameraManager;

	// Use this for initialization
	void Start () {
       alive = true;
       burning = 0;
       cooldown = 0;
       capturing = 0;
       cameraManager = Camera.main.GetComponent<CameraManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (alive) {
            if (lighting) {
                ectoIcon.gameObject.SetActive(false);
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
            } else {
                ectoIcon.gameObject.SetActive(true);
                ectoIcon.transform.GetChild(0).gameObject.SetActive(IsInCooldown());
                if (burning > 0) {
                    burning -= 2 * Time.deltaTime;
                    if (burning < 0) {
                        burning = 0;
                    }
                }
            }
            if (cooldown > 0) {
                cooldown -= Time.deltaTime;
                screenColor.color = Color.Lerp(Color.clear, cooldownColor, cooldown);
            } else if (!shadowing) {
                // cameraManager.Sepia(false);
            }
            if (passingWall) {
               if (recentPassing > 0) {
                    recentPassing -= Time.deltaTime;
               } else {
                    passingWall = false;
               }
            }
            if (capturing > 0) {
                capturingBar.gameObject.SetActive(true);
                capturing -= Time.deltaTime;
                capturingBar.value = capturing;
            } else {
                capturingBar.gameObject.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                SwitchForm();
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
        return shadowing;
    }

    public bool IsInTheLight () {
        return recentDetection > 0;
    }

    public bool IsBurning () {
        return burning > 0.5f;
    }

    public bool IsInCooldown () {
        return cooldown > 0;
    }

    public bool IsBeingCaptured () {
        return capturing > 0;
    }

    public bool IsAlive () {
        return alive;
    }

    void EjectFromShade () {
        ChangeColorFilter(Color.clear);
        shadowing = false;
        burning = 0;
        cooldown = 9;
    }

    void SwitchForm () {
        if (shadowing && !passingWall) {
            ChangeColorFilter(Color.clear);
            shadowing = false;
            burning = 0;
            cooldown = 3;
        } else if (! lighting && cooldown <= 0) {
            shadowing = true;
            // cameraManager.Sepia(true);
        }
    }

    public void Hurt () {
        capturing += 5;
        cameraManager.Blur();
        if (capturing > 15) {
            StartCoroutine("Die");
        }
    }

    IEnumerator Die () {
        alive = false;
        capturing = 15;
        capturingBar.value = capturing;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(defeatScene);
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
