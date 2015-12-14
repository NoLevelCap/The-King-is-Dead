using UnityEngine;
using System.Collections;

public class Army : MonoBehaviour {

    public int StartX = 0, StartY = 0;

    public int x, y;
    public int size = 1;

    public bool Friendly;

    public int MP = 1, BP = 1;

    public bool inBattle = false;

	// Use this for initialization
	void Start () {
        resetArmy();
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(1, 0.05f*(size/2.0f), 1);
        
        if(size<=0){
            resetArmy();
        }

        if(!inBattle){
            UpdateMapPosition();
        }
        

    }

    void UpdateMapPosition()
    {
        transform.localPosition = new Vector3(x*4.5f + 0.5f, -1.5f, y*-4.5f + 0.5f);
    }

    public void setPosition(int nx, int ny)
    {
        if(x+nx < 0)
        {
            x = 0;
        }
        else if(x+nx > 7)
        {
            x = 7;
        } else {
            x += nx;
        }

        if (y + ny < 0)
        {
            y = 0;
        }
        else if (y + ny > 3)
        {
            y = 3;
        }
        else
        {
            y += ny;
        }
    }

    public void buff()
    {
        if (Friendly)
        {
            switch (x)
            {
                case 1:
                    size += 3;
                    break;
                case 2:
                    size += 2;
                    break;
                case 3:
                    size += 1;
                    break;
                case 4:
                    size -= 1;
                    break;
                case 5:
                    size -= 2;
                    break;
                case 6:
                    size -= 3;
                    break;
            }
        }
        else
        {
            switch (x)
            {
                case 1:
                    size -= 3;
                    break;
                case 2:
                    size -= 2;
                    break;
                case 3:
                    size -= 1;
                    break;
                case 4:
                    size += 1;
                    break;
                case 5:
                    size += 2;
                    break;
                case 6:
                    size += 3;
                    break;
            }
        }
    }

    public void resetArmy()
    {
        x = StartX;
        y = StartY;
        size = 1;
    }
}
