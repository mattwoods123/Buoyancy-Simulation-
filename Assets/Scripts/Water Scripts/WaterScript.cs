using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public GameObject Water;
    public GameObject Grounds;
    public GameObject holder;
    public GameObject proj;
    public GameObject projCube;
    public GameObject projCylin;
    public float waterChangeScale = 0.5f;

    int currentObj = 0;

    public float waterDensity = 5;
    // Start is called before the first frame update
    void Start()
    {
        holder = gameObject;
        proj.GetComponent<Particle3D>().water = Water;
        proj.GetComponent<Particle3D>().wd = waterDensity;
        projCube.GetComponent<Particle3D>().water = Water;
        projCube.GetComponent<Particle3D>().wd = waterDensity;
        projCylin.GetComponent<Particle3D>().water = Water;
        projCylin.GetComponent<Particle3D>().wd = waterDensity;
    }
    private void buoyancy(GameObject obj)
    {
        float waterLevel = Water.transform.position.y;
        //buoyancy force should be applied to the object if it is submerged in water
        if (obj.transform.position.y < waterLevel)
        {
            Particle3D par = obj.GetComponent<Particle3D>();
            //calculate the volume of the object
            float volume = (4 / 3) * Mathf.PI * Mathf.Pow(par.getRadius(), 3);
            //calculate the volume of the water that the object is submerged in
            float waterVolume = volume * (waterLevel - gameObject.transform.position.y) / (2 * par.getRadius());
            //calculate the buoyancy force
            float buoyancyForce = waterVolume * waterDensity * par.getGravity().y;
            //apply the buoyancy force to the object
            par.Velocity.y += buoyancyForce / par.getMass() * Time.deltaTime;
            //Debug.Log(buoyancyForce / par.getMass());
            //Debug.Break();
        }
    }

    void fireObj(Vector3 pos)
    {
        switch (currentObj)
        {
            case 0:
                Instantiate(proj, pos, Quaternion.identity, holder.transform);
                break;
            case 1:
                Instantiate(projCube, pos, Quaternion.identity, holder.transform);
                break;
            case 2:
                Instantiate(projCylin, pos, Quaternion.identity, holder.transform);
                break;
            default:
                Debug.Log("ERROR");
                break;
        }
    } 
        // Update is called once per frame
        void Update()
        {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject b = Instantiate(proj, holder.transform.position, Quaternion.identity, holder.transform);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Water.transform.Translate(new Vector3(0, 0,-waterChangeScale * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            Water.transform.Translate(new Vector3(0, 0, waterChangeScale * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.C))
        {
            for (int i = 0; i < holder.transform.childCount; i++)
            {
                Destroy(holder.transform.GetChild(i).gameObject);
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            lookAtMouse();
        }
            if (Input.GetKeyDown(KeyCode.W))
            {
                //Switch Bullet Index
                if (currentObj != 2)
                {
                    currentObj++;
                }
                else
                {
                    currentObj = 0;
                }
            }
        }

    void lookAtMouse()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rHit;

        if (Water.GetComponent<MeshCollider>().Raycast(r, out rHit, 10000))
        {
            fireObj(new Vector3(rHit.point.x, rHit.point.y, rHit.point.z));
        }
    }
}
