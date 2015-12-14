using UnityEngine;
using System.Collections;

public class ChoiceConverter{

    //REMEMBER WARM THEN COLD

    private enum Direction { 
        Forward, Backward, Left, Right
    }

    public bool NoBuff, NoDeBuff, NoBuffNext, NoDeBuffNext;

    private Army Enemy, Player;
    private BoardHandler board;
    private CardHandler handler;
    private BattleHandler battles;

    public ChoiceConverter(CardHandler handler)
    {
        Enemy = GameObject.Find("Enemy Army").GetComponent<Army>();
        Player = GameObject.Find("Player Army").GetComponent<Army>();

        board = GameObject.Find("Map").GetComponent<BoardHandler>();
        battles = GameObject.Find("BattleManager").GetComponent<BattleHandler>();
        this.handler = handler;
    }

    public void HandleCold(Choice.ColdCardTypes card)
    {
        if(!NoDeBuff){
            switch(card){
                case Choice.ColdCardTypes.EArmyBoost:
                    {
                        changeArmy(Enemy, ((int) Mathf.Floor(Enemy.BP / 4) + 1));
                    }
                    break;
                case Choice.ColdCardTypes.EBuildOutpost:
                    {
                        build(Enemy, new Building(Building.Type.Outpost, Enemy));
                    }
                    break;
                case Choice.ColdCardTypes.EBuildVillage:
                    {
                        build(Enemy, new Building(Building.Type.Village, Enemy));
                    }
                    break;
                case Choice.ColdCardTypes.EMoveForward:
                    {
                        moveArmy(Enemy, Direction.Backward, 1);
                    }
                    break;
                case Choice.ColdCardTypes.EMoveLeft:
                    {
                        moveArmy(Enemy, Direction.Left, 1);
                    }
                    break;
                case Choice.ColdCardTypes.EMoveRight:
                    {
                        moveArmy(Enemy, Direction.Right, 1);
                    }
                    break;
                case Choice.ColdCardTypes.NoBuff:
                    {
                        setNoBuff();
                    }
                    break;
                case Choice.ColdCardTypes.PMoveBackwards:
                    {
                        moveArmy(Player, Direction.Backward, 1);
                    }
                    break;
                case Choice.ColdCardTypes.WeakenPlayer:
                    {
                        changeArmy(Player, -((int) Mathf.Floor(Player.BP / 4) + 1));
                    }
                    break;

            }
        }
        else
        {
            //Debug.Log("Cold Card Function Blocked");
            NoDeBuff = false;
        }

        endOfMove();
    }

    public void HandleWarm(Choice.WarmCardTypes card)
         
    {
        if(!NoBuff){
            switch (card)
            {
                case Choice.WarmCardTypes.ArmyBoost:
                    {
                        changeArmy(Player, ((int) Mathf.Floor(Player.BP / 4) + 1));
                    }
                    break;
                case Choice.WarmCardTypes.BuildOutpost:
                    {
                        build(Player, new Building(Building.Type.Outpost, Player));
                    }
                    break;
                case Choice.WarmCardTypes.BuildVillage:
                    {
                        build(Player, new Building(Building.Type.Village, Player));
                    }
                    break;
                case Choice.WarmCardTypes.EMoveBackwards:
                    {
                        moveArmy(Enemy, Direction.Forward, 1);
                    }
                    break;
                case Choice.WarmCardTypes.MoveForward:
                    {
                        moveArmy(Player, Direction.Forward, 1);
                    }
                    break;
                case Choice.WarmCardTypes.MoveLeft:
                    {
                        moveArmy(Player, Direction.Left, 1);
                    }
                    break;
                case Choice.WarmCardTypes.MoveRight:
                    {
                        moveArmy(Player, Direction.Right, 1);
                    }
                    break;
                case Choice.WarmCardTypes.NoDebuff:
                    {
                        setNoDeBuff();
                    }
                    break;
                case Choice.WarmCardTypes.WeakenEnemy:
                    {
                        changeArmy(Enemy, -((int) Mathf.Floor(Enemy.BP / 4) + 1));
                    }
                    break;
            }
        }
        else
        {
            //Debug.Log("Warm Card Function Blocked");
            NoBuff = false;
        }
    }

    private void endOfMove()
    {
        NoDeBuff = NoDeBuffNext;
        NoDeBuffNext = false;

        NoBuff = NoBuffNext;
        NoBuffNext = false;

        Enemy.buff();
        Player.buff();

        board.CountBuilding();

        if(handler.TurnNum % 20 == 0){
            battles.StartBattle();
        }

        handler.EndTurn();
    }

    private void changeArmy(Army army, int amount)
    {
        army.size += amount;
    }

    private void moveArmy(Army army, Direction dir, int amount)
    {
        switch (dir)
        {
            case Direction.Backward:
                army.setPosition(-amount, 0);
                break;
            case Direction.Forward:
                army.setPosition(amount, 0);
                break;
            case Direction.Left:
                army.setPosition(0, -amount);
                break;
            case Direction.Right:
                army.setPosition(0, amount);
                break;
        }
    }

    private void build(Army army, Building bil)
    {
        board.drawBuilding(bil, army);
    }

    private void setNoBuff()
    {
        NoBuffNext = true; 
    }

    private void setNoDeBuff()
    {
        NoDeBuffNext = true;
    }
}
