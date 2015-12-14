using UnityEngine;
using System.Collections;

public class TutorialSceneManager : MonoBehaviour {

    Camera cam;
    public int slide;

	// Use this for initialization
	void Start () {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        slide = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (slide == 1)
            {
                Application.LoadLevel(0);
            }
            else
            {
                slide -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (slide == 3)
            {
                Application.LoadLevel(2);
            }
            else
            {
                slide += 1;
            }
        }

        float step = 35f * Time.deltaTime;
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(70*(slide-1), 1, -10), step);
	}
}
