using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] GameObject sphere;
    [SerializeField] Vector3 pos1;
    [SerializeField] Vector3 pos2;
    bool setPos1, setPos2 = false;
    //[SerializeField] Vector2 force;
    [SerializeField] Camera main;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            pos1 = main.ScreenToWorldPoint(Input.mousePosition);
            pos1.z = main.nearClipPlane;
            setPos1 = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            pos2 = main.ScreenToWorldPoint(Input.mousePosition);
            pos2.z = main.nearClipPlane;
            setPos2 = true;
            
          
        }

        createObject();



    }

    void createObject()
    {
        if( setPos1 && setPos2 )
        {
            Vector3 force3D = pos2 - pos1;
            Vector2 force = new Vector2(force3D.x, force3D.y).normalized;
            GameObject myBall = sphere;
            myBall.GetComponent<P2>().setVelocity(force);
            Instantiate(sphere, pos1, Quaternion.identity);
            setPos1 = false;
            setPos2 = false;
            Debug.Log(force);
        } 
    }

}
