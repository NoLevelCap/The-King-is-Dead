using UnityEngine;
using System.Collections;

public static class Choice {
    private static string[] WarmStringAssoc = {
                                       "+[CHANGE] To your Army",
                                       "Move 1 Left",
                                       "Move 1 Right",
                                       "Move 1 Forward",
                                       "Your Army builds an Outpost",
                                       "Your Army builds an Village",
                                       "-[CHANGE] to Enemy Army",
                                       "The next debuff doesn't work",
                                       "Enemy moves 1 Backwards",
                                   };

    private static string[] ColdStringAssoc = {
                                       "-[CHANGE] To your Army",
                                       "Enemy moves 1 Left",
                                       "Enemy moves 1 Right",
                                       "Enemy moves 1 Forward",
                                       "The Enemy Army builds an Outpost",
                                       "The Enemy Army builds an Village",
                                       "-[CHANGE] to your Army",
                                       "The next buff doesn't work",
                                       "Move 1 Backward",
                                   };

    public enum WarmCardTypes : int
    {
        ArmyBoost, MoveLeft, MoveRight, MoveForward, BuildOutpost, BuildVillage, WeakenEnemy, NoDebuff, EMoveBackwards

    }

    public enum ColdCardTypes
    {
        EArmyBoost, EMoveLeft, EMoveRight, EMoveForward, EBuildOutpost, EBuildVillage, WeakenPlayer, NoBuff, PMoveBackwards
    }

    public static WarmCardTypes DrawRandomWarm()
    {
        WarmCardTypes Card = (WarmCardTypes)Random.Range(0, System.Enum.GetValues(typeof(WarmCardTypes)).GetLength(0));
        return Card;
    }

    public static ColdCardTypes DrawRandomCold()
    {
        ColdCardTypes Card = (ColdCardTypes)Random.Range(0, System.Enum.GetValues(typeof(ColdCardTypes)).GetLength(0));
        return Card;
    }

    public static string WarmArrayAssoc(int i){
        return WarmStringAssoc[i];
    }

    public static string ColdArrayAssoc(int i)
    {
        return ColdStringAssoc[i];
    }

    public static string replaceWarmInt(Army army, Army enemy, WarmCardTypes card){
        string returnString = WarmArrayAssoc((int) card);
        switch (card)
        {
            case Choice.WarmCardTypes.ArmyBoost:
                {
                    returnString = WarmArrayAssoc((int)card).Replace("[CHANGE]", (Mathf.Floor(army.BP / 4) + 1).ToString());
                }
                break;
            case Choice.WarmCardTypes.WeakenEnemy:
                {
                    returnString = WarmArrayAssoc((int)card).Replace("[CHANGE]", (Mathf.Floor(enemy.BP / 4) + 1).ToString());
                }
                break;
        }
        return returnString;
    }

    public static string replaceColdInt(Army enemy, Army player, ColdCardTypes card)
    {
        string returnString = ColdArrayAssoc((int)card);
        switch (card)
        {
            case Choice.ColdCardTypes.EArmyBoost:
                {
                    returnString = ColdArrayAssoc((int)card).Replace("[CHANGE]", (Mathf.Floor(enemy.BP / 4) + 1).ToString());
                }
                break;
            case Choice.ColdCardTypes.WeakenPlayer:
                {
                    returnString = ColdArrayAssoc((int)card).Replace("[CHANGE]", (Mathf.Floor(player.BP / 4) + 1).ToString());
                }
                break;
        }
        return returnString;
    }

}
