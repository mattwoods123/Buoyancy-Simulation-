using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public float radius = 0.5f;
    public GameObject projHolder;
    public float diameter = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
    // Update is called once per frame
    void Update()
    {
        didHit();
    }
    void didHit()
    {
        for(int i = 0; i < projHolder.transform.childCount; i++)
        {
            Vector2 pos = projHolder.transform.GetChild(i).transform.position;
            Vector2 distance = pos - (Vector2)transform.position;
            if(distance.magnitude < diameter)
            {
                Destroy(gameObject);
            }
        }
    }
}
