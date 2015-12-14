using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroController : MonoBehaviour {

    Image MainCam;

	// Use this for initialization
	void Start () {
        MainCam = GameObject.Find("DarkPanel").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        MainCam.color = Color.Lerp(Color.black, new Color(0, 0, 0, 0), Time.time/4);
	}
}
