using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoardHandler : MonoBehaviour {

    public GameObject[,] board;

    public Building[,] buildings;

    public Army Enemy, Player;


    public BattleHandler battles;

    private GameObject OPOutpost, OPVillage, OEOutpost, OEVillage;

    private Image FadeIn;

    public enum TileType
    {
        Land, Home, EnemyHome
    }

	// Use this for initialization
	void Start () {
        board = new GameObject[8, 4];
        buildings = new Building[8, 4];

        Enemy = GameObject.Find("Enemy Army").GetComponent<Army>();
        Player = GameObject.Find("Player Army").GetComponent<Army>();

        battles = GameObject.Find("BattleManager").GetComponent<BattleHandler>();

        OPOutpost = GameObject.Find("POutpost");
        OPVillage = GameObject.Find("PVillage");
        OEOutpost = GameObject.Find("EOutpost");
        OEVillage = GameObject.Find("EVillage");

        FadeIn = GameObject.Find("FadeIn").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckArmiesPosition();
	}

    void CheckArmiesPosition()
    {
           if(Enemy.x == Player.x && Enemy.y == Player.y){
               battles.StartBattle();
               battles.lossArmy.resetArmy();
           }

        
    }

    public void drawBuilding(Building bil, Army owner)
    {
        if (board[owner.x, owner.y] != null)
        {
            Destroy(board[owner.x, owner.y]);
        }
        GameObject copy = OPVillage;
        switch (bil.type)
        {
            case Building.Type.Outpost:
                if(bil.Friendly){
                    copy = OPOutpost;
                }
                else
                {
                    copy = OEOutpost;
                }
                break;
            case Building.Type.Village:
                if (bil.Friendly)
                {
                    copy = OPVillage;
                }
                else
                {
                    copy = OEVillage;
                }
                break;
        }
        board[owner.x, owner.y] = (GameObject)Instantiate(copy, new Vector3(0, 0, 0), Quaternion.identity);
        board[owner.x, owner.y].transform.parent = transform;
        bil.owner = board[owner.x, owner.y];
        board[owner.x, owner.y].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        board[owner.x, owner.y].transform.localPosition = new Vector3(owner.x * 0.0225f, owner.y * 0.0225f, -0.001f);
        buildings[owner.x, owner.y] = bil;
        
    }

    public void CountBuilding()
    {
        Player.BP = 0;
        Player.MP = 0;
        Enemy.BP = 0;
        Enemy.MP = 0;
        foreach(Building bild in buildings){
            if (bild != null)
            {
                Army select;
                if (bild.Friendly)
                {
                    select = Player;
                }
                else
                {
                    select = Enemy;
                }

                switch (bild.type)
                {
                    case Building.Type.Outpost:
                        select.BP += 4;
                        break;
                    case Building.Type.Village:
                        select.MP += 1;
                        select.BP += 1;
                        break;
                }
            }
        }
    }
}
