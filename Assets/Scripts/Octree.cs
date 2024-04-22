using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octree : MonoBehaviour
{
    public Octree parent;
    public Octree[] children;
    public List<GameObject> objects;
    public float size;
    public Vector3 center;
    public int collisions = 0;

    public Octree(Octree parent, float size, Vector3 center)
    {
        this.parent = parent;
        this.size = size;
        this.center = center;
        objects = new List<GameObject>();
    }

    public void Add(GameObject obj)
    {
        objects.Add(obj);
    }

    public void Subdivide()
    {
        float half = size / 2;
        children = new Octree[8];
        children[0] = new Octree(this, half, center + new Vector3(half, half, half));
        children[1] = new Octree(this, half, center + new Vector3(half, half, -half));
        children[2] = new Octree(this, half, center + new Vector3(half, -half, half));
        children[3] = new Octree(this, half, center + new Vector3(half, -half, -half));
        children[4] = new Octree(this, half, center + new Vector3(-half, half, half));
        children[5] = new Octree(this, half, center + new Vector3(-half, half, -half));
        children[6] = new Octree(this, half, center + new Vector3(-half, -half, half));
        children[7] = new Octree(this, half, center + new Vector3(-half, -half, -half));

        for (int i = 0; i < objects.Count; i++)
        {
            for (int j = 0; j < children.Length; j++)
            {
                if (children[j].Contains(objects[i]))
                {
                    children[j].Add(objects[i]);
                    objects.RemoveAt(i);
                    i--;
                    break;
                }
            }
        }
    }

    public bool Contains(GameObject obj)
    {
        Vector3 pos = obj.transform.position;
        if (pos.x <= center.x + size / 2 && pos.x >= center.x - size / 2 &&
              pos.y <= center.y + size / 2 && pos.y >= center.y - size / 2 &&
              pos.z <= center.z + size / 2 && pos.z >= center.z - size)
        {
            collisions++;
            return true;
        }
        else
        {
            return false;
        }

    }
}
