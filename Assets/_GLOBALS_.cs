using UnityEngine;
using System.Collections;

public class _GLOBALS_ : MonoBehaviour {

    public static int Turns;
    public static bool Win;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
