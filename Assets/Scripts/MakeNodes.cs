using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNodes : MonoBehaviour
{
    public GameObject nodes;
    public GameObject holder;
    public float distanceRange = 8;
    bool togglePause = true;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 25; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-distanceRange, distanceRange), Random.Range(-distanceRange, distanceRange), Random.Range(-distanceRange, distanceRange));
            Vector3 vel = new Vector3(Random.Range(-distanceRange, distanceRange), Random.Range(-distanceRange, distanceRange), Random.Range(-distanceRange, distanceRange));
            GameObject g = Instantiate(nodes, transform.position + pos, Quaternion.identity, holder.transform);

            g.GetComponent<Particle3D>().Velocity = vel;
        }
        Time.timeScale = 0;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && togglePause)
        {
            Time.timeScale = 1;
            togglePause = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !togglePause)
        {
            Time.timeScale = 0;
            togglePause = true;
        }
    }

}
