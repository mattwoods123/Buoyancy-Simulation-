using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB : MonoBehaviour
{
    public Vector3 min;
    public Vector3 max;

    public AABB(Transform cube)
    {
        Vector3[] poses = new Vector3[8];
        //Vector3.Scale(cube.localScale / 2, new Vector3(1, 1, 1))
        poses[0] = cube.TransformPoint(new Vector3(0.5f, 0.5f, 0.5f));
        poses[1] = cube.TransformPoint(new Vector3(-0.5f, 0.5f, 0.5f));
        poses[2] = cube.TransformPoint(new Vector3(0.5f, -0.5f, 0.5f));
        poses[3] = cube.TransformPoint(new Vector3(0.5f, 0.5f, -0.5f));
        poses[4] = cube.TransformPoint(new Vector3(0.5f, -0.5f,- 0.5f));
        poses[5] = cube.TransformPoint(new Vector3(-0.5f, -0.5f, 0.5f));
        poses[6] = cube.TransformPoint(new Vector3(-0.5f, 0.5f, -0.5f));
        poses[7] = cube.TransformPoint(new Vector3(-0.5f, -0.5f, -0.5f));

        min = poses[0];
        max = poses[7];

        for (int i = 0; i < 8; i++)
        {
            if (poses[i].y < min.y)
                min.y = poses[i].y;
            if(poses[i].x < min.x)
                min.x = poses[i].x;
            if (poses[i].z < min.z)
                min.z = poses[i].z;
        }

        for (int i = 0; i < 8; i++)
        {
            if (poses[i].y > max.y)
                max.y = poses[i].y;
            if (poses[i].x > max.x)
                max.x = poses[i].x;
            if (poses[i].z > max.z)
                max.z = poses[i].z;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
