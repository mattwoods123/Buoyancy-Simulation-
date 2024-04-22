using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 5;
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject bullet1;
    [SerializeField] GameObject bullet2;
    [SerializeField] GameObject bullet3;
    [SerializeField] GameObject bulletHolder;
    [SerializeField] BoxCollider backDrop;
    [SerializeField] int currentBullet = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < bulletHolder.transform.childCount; i++)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos -= (Vector2)bulletHolder.transform.GetChild(i).position;
                bulletHolder.transform.GetChild(i).GetComponent<Particle2D>().addForce(pos.normalized);
            }
        }
        if (Input.GetMouseButton(1))
        {
            for (int i = 0; i < bulletHolder.transform.childCount; i++)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos -= (Vector2)bulletHolder.transform.GetChild(i).position;
                bulletHolder.transform.GetChild(i).GetComponent<Particle2D>().addForce(-pos.normalized);
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < bulletHolder.transform.childCount; i++)
            {
                Destroy(bulletHolder.transform.GetChild(i).gameObject);
            }
        }
         
         if (Input.GetKey(KeyCode.Alpha1))
        {
            //Rotate Up
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime, Space.World);

        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            //Rotate Down
            transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime, Space.World);
        }
        
        //lookAtMouse();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Fire Bullet
            fireBullet();
     
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //Switch Bullet Index
            if (currentBullet != 2)
            {
                currentBullet++;
            }
            else
            {
                currentBullet = 0;
            }
        }
    }
    void fireBullet()
    {
        switch (currentBullet)
        {
            case 0:

                GameObject b1 = Instantiate(bullet1, firePoint.transform.position, Quaternion.identity, bulletHolder.transform);
                b1.GetComponent<Particle2D>().Velocity = Vector2.zero;
                b1.GetComponent<Particle2D>().Velocity = transform.up * 5;
                Debug.Log(transform.up);
                Debug.Log(b1.GetComponent<Particle2D>().Velocity);
                //Debug.Break();

                b1.GetComponent<Particle2D>().springAnchor = GameObject.Find("SpringJoint");

                break;
            case 1:

                GameObject b2 = Instantiate(bullet2, firePoint.transform.position, Quaternion.identity);

                GameObject ball1 = b2.transform.GetChild(0).gameObject;
                GameObject ball2 = b2.transform.GetChild(1).gameObject;
                

                ball1.GetComponent<Particle2D>().Velocity = transform.up * 5;
                ball1.GetComponent<Particle2D>().Velocity.y += 3;
                ball2.GetComponent<Particle2D>().Velocity = transform.up * 5;
                ball2.GetComponent<Particle2D>().Velocity.y += -3;
                ball1.transform.parent = bulletHolder.transform;
                ball2.transform.parent = bulletHolder.transform;

                Destroy(b2);

                break;
            case 2:

                GameObject b3 = Instantiate(bullet3, firePoint.transform.position, Quaternion.identity, bulletHolder.transform);
                b3.GetComponent<Particle2D>().Velocity = transform.up * 5;

                break;
            default:
                Debug.Log("ERROR");
                break;
        }
    }
    void lookAtMouse()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rHit;
        
        if (backDrop.Raycast(r, out rHit, 10000))
        {
            Quaternion look = Quaternion.LookRotation(rHit.point - transform.position, Vector3.right);

            //Quaternion noX = Quaternion.Euler(0, look.y * Mathf.Rad2Deg, look.z * Mathf.Rad2Deg);

            gameObject.transform.rotation = look;
            Debug.Log(rHit.point);
            //gameObject.transform.Rotate(new Vector3(0,0,-90), Space.World);
        }

        
    }

}
