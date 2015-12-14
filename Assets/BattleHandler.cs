using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleHandler : MonoBehaviour {

    GameObject Enemy, Player, Map, MapCanvas, Battle, Keys, Options, RD, LD;

    Text PP, EP;

    Army EnemyA, PlayerA;

    public bool BattleRunning = false, BattleEnding = false;

    private int newLossSize;

    public Vector3 PlayerPos, EnemyPos;

    public Army lossArmy;

	// Use this for initialization
	void Start () {
        Battle = GameObject.Find("Battle");
        Enemy = GameObject.Find("Enemy Army");
        Player = GameObject.Find("Player Army");
        Map = GameObject.Find("Map");
        MapCanvas = GameObject.Find("BoardCanvas");
        Keys = GameObject.Find("KeyHolder");
        Options = GameObject.Find("ExampleHolder");
        RD = GameObject.Find("d6R");
        LD = GameObject.Find("d6R");
        PP = GameObject.Find("PP").GetComponent<Text>();
        EP = GameObject.Find("EP").GetComponent<Text>();
        EnemyA = Enemy.GetComponent<Army>();
        PlayerA = Player.GetComponent<Army>();

        PP.transform.gameObject.SetActive(false);
        EP.transform.gameObject.SetActive(false);

        Battle.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.B)){
            if(!BattleRunning){
                StartBattle();
            }
            else
            {
                StartToEnd();
            }
        }

        if(BattleRunning && !BattleEnding){
            DoBattle();
        }

        if(BattleEnding){
            CloseBattle();
        }
	}

    public void StartBattle()
    {
        BattleRunning = true;
        Map.SetActive(false);
        MapCanvas.SetActive(false);
        Keys.SetActive(false);
        Options.SetActive(false);
        Battle.SetActive(true);
        EnemyA.inBattle = true;
        PlayerA.inBattle = true;
        newLossSize = -1;
        PlayerPos = Player.transform.position;
        EnemyPos = Enemy.transform.position;
    }

    float wait = 1.5f;
    float lasttime;

    public void StartToEnd()
    {
        if(lasttime == 0){
            lasttime = Time.time;
        }

        if(Time.time - lasttime > wait){
            lasttime = 0;
            BattleEnding = true;
        }
        
    }

    public void EndBattle()
    {
        BattleRunning = false;
        Map.SetActive(true);
        MapCanvas.SetActive(true);
        Keys.SetActive(true);
        Options.SetActive(true);
        Battle.SetActive(false);
        EnemyA.inBattle = false;
        PlayerA.inBattle = false;
        PP.transform.gameObject.SetActive(false);
        EP.transform.gameObject.SetActive(false);
        BattleEnding = false;
        Chance = 0;
    }

    public void DoBattle()
    {
        if (Player.transform.position != new Vector3(5 - 17, -4, -7) || Enemy.transform.position != new Vector3(1, -4, -19) || Battle.transform.localScale != new Vector3(10, 10, 0))
        {
            float step = 35f * Time.deltaTime;
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, new Vector3(5-17, -4, -7), step);
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, new Vector3(1, -4, -19), step);

            Battle.transform.localScale = Vector3.MoveTowards(Battle.transform.localScale, new Vector3(10, 10, 0), step);
        }
        else
        {
            CalculateFight();
            shrinkArmy(lossArmy);
        }
    }

    public void CloseBattle()
    {
        if (Player.transform.position != PlayerPos || Enemy.transform.position != EnemyPos || Battle.transform.localScale != new Vector3(0, 0, 0))
        {
            float step = 35f * Time.deltaTime;
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerPos, step);
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, EnemyPos, step);

            Battle.transform.localScale = Vector3.MoveTowards(Battle.transform.localScale, new Vector3(0, 0, 0), step);
        }
        else
        {
            EndBattle();
        }
    }

    private void shrinkArmy(Army a){

        if (newLossSize < a.size)
        {
            a.size -= (int)((100 * Time.deltaTime));
            if (newLossSize >= PlayerA.size && PlayerA.size-newLossSize>=0)
            {
                StartToEnd();
                a.size = newLossSize;
            }
        }
        else
        {
            StartToEnd();
            a.size = newLossSize;
        }
    }

    float Chance = 0;

    public void CalculateFight()
    {

        if(Chance == 0){
            PP.transform.gameObject.SetActive(true);
            EP.transform.gameObject.SetActive(true);

            float totalMp = EnemyA.MP+1 + PlayerA.MP+1;
            float totalMen = EnemyA.size + PlayerA.size;



            float EnemyChance = (((EnemyA.MP+1) / totalMp) + (EnemyA.size / totalMen)) / 2;
            float PlayerChance = (((PlayerA.MP+1) / totalMp) + (PlayerA.size / totalMen)) / 2;

            Chance = Random.Range(0, 100)/100f;

            PP.text = Mathf.Round(PlayerChance*100).ToString() + "%";
            EP.text = Mathf.Round(EnemyChance * 100).ToString() + "%";

            if (Chance < PlayerChance)
            {
                newLossSize = Mathf.RoundToInt(EnemyA.size / 2);
                lossArmy = EnemyA;
            } else {
                newLossSize = Mathf.RoundToInt(PlayerA.size / 2);
                lossArmy = PlayerA;
            }
        }
    }

    /*private void RightDicePos(int num)
    {
        switch(num)
        {
            case 1:
                RD.transform.localRotation = new Quaternion(-56, -58, -42, RD.transform.);
                break;
        }
    }*/
}
