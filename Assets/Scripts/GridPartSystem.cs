using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridPartSystem : MonoBehaviour
{
    public GameObject projHolder;

    public int gridSize = 10;
    public int cellSize = 10;

    public Vector3 origin = Vector3.zero;

    public GridNode[,] grid;



    private void Start()
    {
        grid = new GridNode[gridSize, gridSize];
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                grid[i, j] = new GridNode();
            }
                
        }
        origin = gameObject.transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + new Vector3(gridSize * cellSize, 0, 0));
        Gizmos.DrawLine(origin, origin + new Vector3(0, gridSize * cellSize, 0));
    }

    public void placeObject(GameObject g)
    {
        Vector3 pos = g.transform.localPosition;

        int x = 0;

        bool[] walls = new bool[4];

        for(int i = 1; i < gridSize; i++)
        {
            if (pos.x <= (origin.x + (cellSize * i)) && pos.x >= (origin.x + (cellSize * (i-1))))
            {
                x = i - 1;
            }
            
            float dif = getCenterPos(x, 0).x - pos.x;
            if (Mathf.Abs(dif) > cellSize/4)
            {
                if(dif > 0 && x != gridSize)
                {
                    walls[3] = true;
                }
                else if (x != 0)
                {
                    walls[2] = true;
                }
            }
        }

        int y = 0;

        for (int i = 0; i < gridSize; i++)
        {
            if (pos.y <= (origin.y + (cellSize * i)) && pos.y >= (origin.y + (cellSize * (i - 1))))
            {
                y = i - 1;
            }
            float dif = getCenterPos(y, 0).y - pos.y;
            if (Mathf.Abs(dif) > cellSize / 4)
            {
                if (dif > 0 && y != gridSize)
                {
                    walls[1] = true;
                }
                else if(y != 0)
                {
                    walls[0] = true;
                }
            }
        }
        //Debug.Log(pos);
        ////Debug.Log((origin.x + (cellSize * x)));
        ////Debug.Log((origin.y + (cellSize * y)));
        ////Debug.Break();
        //Debug.Log(x);
        //Debug.Log(y);
        grid[x, y].myObjs.Add(g);
        grid[x, y].close = walls;
    }

    Vector2 getCenterPos(int x, int y)
    {
        return new Vector2(origin.x + (cellSize * x) + cellSize / 2, origin.y + (cellSize * y) + cellSize / 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
 
        for (int i = 0; i < gridSize; i++)
        {
            for(int j = 0; j < gridSize; j++)
            {

                if(grid[i,j].myObjs.Count != 0)
                {
                    
                    for (int h = 0; h < grid[i, j].myObjs.Count; h++)
                    {

                        List<GameObject> c = new List<GameObject>();
                        for (int t = 0; t < grid[i, j].myObjs.Count; t++)
                        {
                            
                            if (grid[i, j].myObjs[h].GetComponent<Particle3D>().checkOverlap(grid[i, j].myObjs[h], grid[i, j].myObjs[t]))
                            {
                                c.Add(grid[i, j].myObjs[t]);
                            }
                        }

                        if (grid[i, j].close[0] == true)
                        {
                            for (int t = 0; t < grid[i, j + 1].myObjs.Count; t++)
                            {
                                if (grid[i, j].myObjs[h].GetComponent<Particle3D>().checkOverlap(grid[i, j].myObjs[h], grid[i, j + 1].myObjs[t]))
                                {
                                    c.Add(grid[i, j + 1].myObjs[t]);
                                }
                            }
                        }
                        else if (grid[i, j].close[1] == true)
                        {
                            for (int t = 0; t < grid[i, j - 1].myObjs.Count; t++) // this one bad
                            {
                                if (grid[i, j].myObjs[h].GetComponent<Particle3D>().checkOverlap(grid[i, j].myObjs[h], grid[i, j - 1].myObjs[t]))
                                {
                                    c.Add(grid[i, j - 1].myObjs[t]);
                                }
                            }
                        }
                        else if (grid[i, j].close[2] == true)
                        {
                            for (int t = 0; t < grid[i + 1, j].myObjs.Count; t++)
                            {
                                if (grid[i, j].myObjs[h].GetComponent<Particle3D>().checkOverlap(grid[i, j].myObjs[h], grid[i + 1, j].myObjs[t]))
                                {
                                    c.Add(grid[i + 1, j].myObjs[t]);
                                }
                            }
                        }
                        else if (grid[i, j].close[3] == true)
                        {
                            for (int t = 0; t < grid[i - 1, j].myObjs.Count; t++)
                            {
                                if (grid[i, j].myObjs[h].GetComponent<Particle3D>().checkOverlap(grid[i, j].myObjs[h], grid[i - 1, j].myObjs[t]))
                                {
                                    c.Add(grid[i - 1, j].myObjs[t]);
                                }
                            }
                        }

                        grid[i, j].myObjs[h].GetComponent<Particle3D>().colCheck = c;
                        //Debug.Log(i+ " " + j);
                        //Debug.Log(grid[i, j].myObjs[h].name);
                        ////Debug.Log();


                    }
                    grid[i, j].myObjs.Clear();
                }
                
            }
        }
        for (int i = 0; i < projHolder.transform.childCount; i++)
        {
            placeObject(projHolder.transform.GetChild(i).gameObject);
        }
    }
}
