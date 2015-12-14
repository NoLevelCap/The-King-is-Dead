using UnityEngine;
using System.Collections;

public class Building{

    public int x = 1, y = 0;
    public Type type = Type.Outpost;
    public GameObject owner;
    public bool Friendly;

    public enum Type
    {
        Empty, Outpost, Village
    }

    public Building(Type ttype, int x, int y, bool Friendly)
    {
        type = ttype;
    }

    public Building(Type ttype, Army parent)
    {
        Friendly = parent.Friendly;
        type = ttype;
    }

}
