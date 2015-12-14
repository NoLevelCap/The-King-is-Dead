using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasFollow : MonoBehaviour {

    GameObject ECanvas, PCanvas, Enemy, Player;

    Text[] EnemyText, PlayerText;

	// Use this for initialization
	void Start () {
        ECanvas = GameObject.Find("EnemyCanvas");
        PCanvas = GameObject.Find("PlayerCanvas");
        Enemy = GameObject.Find("Enemy Army");
        Player = GameObject.Find("Player Army");
        EnemyText = new Text[ECanvas.transform.childCount];
        PlayerText = new Text[PCanvas.transform.childCount];

        int a = 0;
        foreach(Text text in ECanvas.GetComponentsInChildren<Text>()){
            EnemyText[a] = text;
             a++;
        }

        a = 0;
        foreach (Text text in PCanvas.GetComponentsInChildren<Text>())
        {
            PlayerText[a] = text;
            a++;
        }
	}
	
	// Update is called once per frame
	void Update () {
        alignCanvi();
        UpdatePointers();
	}

    void alignCanvi()
    {
        ECanvas.transform.localPosition = new Vector3(Enemy.transform.localPosition.x - 3.5f, Enemy.transform.localPosition.y + 2f, Enemy.transform.localPosition.z - 3.5f);
        PCanvas.transform.localPosition = new Vector3(Player.transform.localPosition.x - 3.5f, Player.transform.localPosition.y + 2f, Player.transform.localPosition.z - 3.5f);
    }

    void UpdatePointers()
    {
        Army EnemyArmy = Enemy.GetComponent<Army>();
        EnemyText[0].text = "MP: "+ EnemyArmy.MP;
        EnemyText[1].text = "BP: " + EnemyArmy.BP;
        EnemyText[2].text = "S: " + EnemyArmy.size;

        Army PlayerArmy = Player.GetComponent<Army>();
        PlayerText[0].text = "MP: " + PlayerArmy.MP;
        PlayerText[1].text = "BP: " + PlayerArmy.BP;
        PlayerText[2].text = "S: " + PlayerArmy.size;
    }
}
