using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGameH : MonoBehaviour {

    Text WL, Turns, Score;

	// Use this for initialization
	void Start () {
        WL = GameObject.Find("Text").GetComponent<Text>();
        Turns = GameObject.Find("Name").GetComponent<Text>();
        Score = GameObject.Find("Score").GetComponent<Text>();

        WL.text = WL.text.Replace("[W]", _GLOBALS_.Win ? "Won" : "Lost").Replace("[F]", _GLOBALS_.Win ? ":)" : ":(");
        Turns.text = Turns.text.Replace("X", _GLOBALS_.Turns.ToString());
        Score.text = getScore(_GLOBALS_.Win, _GLOBALS_.Turns);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(_GLOBALS_.Win + "/ " + _GLOBALS_.Turns);
	}

    string getScore(bool Win, int Turns){
        if(Win){
            if(Turns > 1600){
                return "F";
            }
            else if (Turns > 800)
            {
                return "D";
            }
            else if (Turns > 400)
            {
                return "C";
            }
            else if (Turns > 200)
            {
                return "B";
            }
            else if (Turns > 100)
            {
                return "A";
            }
            else if (Turns > 50)
            {
                return "A+";
            }
            else if (Turns > 25)
            {
                return "S";
            }
            else 
            {
                return "S+";
            }
        } else {
            return "F-";
        }
    }
}
