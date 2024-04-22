using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode
{
    public List<GameObject> myObjs;
    // 0 up 1 down 2 right 3 left
    public bool[] close;

    public GridNode()
    {
       myObjs = new List<GameObject>();
        close = new bool[4];
        for (int i = 0; i < 4; i++)
            close[i] = false;
    }
}
