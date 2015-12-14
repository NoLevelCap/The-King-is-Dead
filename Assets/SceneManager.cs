using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneManager: MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)){
            Application.LoadLevel(1);
        }
	}

}
