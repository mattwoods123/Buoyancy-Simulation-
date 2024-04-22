using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    public GameObject bullet1;
    public GameObject projHolder;
    public Vector3 target = new Vector3(0,3,0);
    public float timerMax = 1;
    public float timer = 1;
    // Start is called before the first frame update
    void Start()
    {
        projHolder = GameObject.Find("ProjectileHolder");
   
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = timerMax;
            GameObject b = bullet1;
            b.GetComponent<Particle2D>().Velocity = (Vector2)(target - gameObject.transform.position).normalized *10;
            Instantiate(b, gameObject.transform.position, Quaternion.identity, projHolder.transform);
        }
    }
}
