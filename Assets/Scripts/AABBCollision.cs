using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABBCollision : MonoBehaviour
{
    AABB boundingBox;
    GameObject projHolder;
    // Start is called before the first frame update
    private void Awake()
    {
        boundingBox = new AABB(transform);
    }
    void Start()
    {
       
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(boundingBox.min, boundingBox.max);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    bool didCollide()
    {
        for (int i = 0; i < projHolder.transform.childCount; i++)
        {
            Transform ball = projHolder.transform.GetChild(i);
        }

        return false;
    }


    Vector3 getContactPoint(Vector3 point)
    {
        Vector3 closestPoint = point;

        closestPoint.x = Mathf.Clamp(closestPoint.x, boundingBox.min.x, boundingBox.max.x);
        closestPoint.y = Mathf.Clamp(closestPoint.y, boundingBox.min.y, boundingBox.max.y);
        closestPoint.z = Mathf.Clamp(closestPoint.z, boundingBox.min.z, boundingBox.max.z);

        return closestPoint;
    }
}
