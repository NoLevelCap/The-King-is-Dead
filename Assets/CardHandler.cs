using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour {

    private GameObject WarmL, ColdL, WarmR, ColdR;

    private BattleHandler battle;

    private Choice.WarmCardTypes WL, WR;
    private Choice.ColdCardTypes CL, CR;

    private GameObject Key_A, Key_D, TN, BN;
    private Text Turns;

    public ChoiceConverter choice;

    private bool chosen;



    private Army Enemy, Player;

    public bool NB, NDB, NBN, NDBN;

    public int TurnNum = 1;


	// Use this for initialization
	void Start () {
        WarmL = GameObject.Find("W:1");
        ColdL = GameObject.Find("C:1");
        WarmR = GameObject.Find("W:2");
        ColdR = GameObject.Find("C:2");
        Key_A = GameObject.Find("A Key");
        Key_D = GameObject.Find("D Key");
        TN = GameObject.Find("TOPNO");
        BN = GameObject.Find("BOTNO");
        choice = new ChoiceConverter(this);
        Enemy = GameObject.Find("Enemy Army").GetComponent<Army>();
        Player = GameObject.Find("Player Army").GetComponent<Army>();
        battle = GameObject.Find("BattleManager").GetComponent<BattleHandler>();
        Turns = GameObject.Find("Turn Counter").GetComponent<Text>();  
        GenerateNewCards();

        TN.SetActive(false);
        BN.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (!battle.BattleRunning) {

            Turns.text = "T:" +  TurnNum;

            

            if (NB)
            {
                TN.SetActive(true);
            }
            else
            {
                TN.SetActive(false);
            }

            if (NDB)
            {
                BN.SetActive(true);
            }
            else
            {
                BN.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.A) )
            {
                LeftChoiceSelected();
                chosen = true;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                RightChoiceSelected();
                chosen = true;
            }

            if(chosen){
                GenerateNewCards();
                chosen = false;
            }

            NB = choice.NoBuff;
            NDB = choice.NoDeBuff;
            NBN = choice.NoBuffNext;
            NDBN = choice.NoDeBuffNext;

            
        }
	}

    void LeftChoiceSelected()
    {
        AffectGame(true);
    }

    void RightChoiceSelected()
    {
        AffectGame(false);
    }

    private void setDarker(GameObject key)
    {
        MeshRenderer renderer = (MeshRenderer)key.GetComponent(typeof(MeshRenderer));
        renderer.material.color = new Color(0.2f, 0.2f, 0.2f, 0.2f);
    }

    private void setLighter(GameObject key)
    {
        MeshRenderer renderer = (MeshRenderer)key.GetComponent(typeof(MeshRenderer));
        renderer.material.color = new Color(0, 0, 0, 0);
    }

    void GenerateNewCards()
    {
        WL = Choice.DrawRandomWarm();
        WR = Choice.DrawRandomWarm();
        CL = Choice.DrawRandomCold();
        CR = Choice.DrawRandomCold();
        //Debug.Log("Warm Cards: '" + WL + "'/'" + WR + "'");
        //Debug.Log("Cold Cards: '" + CL + "'/'" + CR + "'");
        setOptions();
    }

    float timeLimit = 10.0f;

    void checkTimer()
    {
        // translate object for 10 seconds.
        if (timeLimit > 1)
        {
            // Decrease timeLimit.
            timeLimit -= Time.deltaTime;
            // translate backward.
            transform.Translate(Vector3.back * Time.deltaTime, Space.World);
            Debug.Log(timeLimit);
        }
    }

    void setOptions()
    {
        Text textWL = (Text)WarmL.GetComponent("Text");
        textWL.text = Choice.replaceWarmInt(Player, Enemy, WL);

        Text textWR = (Text)WarmR.GetComponent("Text");
        textWR.text = Choice.replaceWarmInt(Player, Enemy, WR);

        Text textCL = (Text)ColdL.GetComponent("Text");
        textCL.text = Choice.replaceColdInt(Player, Enemy, CL);

        Text textCR = (Text)ColdR.GetComponent("Text");
        textCR.text = Choice.replaceColdInt(Player, Enemy, CR);
    }

    

    private void AffectGame(bool left)
    {
        if(left){
            choice.HandleWarm(WL);
            choice.HandleCold(CL);
        }
        else
        {
            choice.HandleWarm(WR);
            choice.HandleCold(CR);
        }
    }

    public void EndTurn(){
        TurnNum += 1;
        _GLOBALS_.Turns = TurnNum;

        if (Enemy.x == 0)
        {
            _GLOBALS_.Win = false;
            Application.LoadLevel(3);
        }

        if (Player.x == 7)
        {
            _GLOBALS_.Win = true;
            Application.LoadLevel(3);

        }
    }

   
}
