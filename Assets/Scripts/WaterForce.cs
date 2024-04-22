using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum shapeType
{
    sphere, cube, cylinder
}

public class WaterForce : ForceGenerator
{
    public GameObject water;
    public float waterDensity;
    public shapeType st;


    public WaterForce(GameObject otherPar, float wd, shapeType shape)
    {
        this.water = otherPar;
        waterDensity = wd;
        st = shape;
    }


    public override void updateForce(Particle3D par, float duration)
    {
        float waterLevel = water.transform.position.y;
        GameObject obj = par.gameObject;
        //buoyancy force should be applied to the object if it is submerged in water

        switch(st) {
            case shapeType.sphere:
                if (obj.transform.position.y - par.getRadius() < waterLevel)
                {

                    //calculate the volume of the object
                    //float volume = (4 / 3) * Mathf.PI * Mathf.Pow(par.getRadius(), 3);
                    //calculate the volume of the water that the object is submerged in
                    float waterVolume;
                    if (waterLevel - (obj.transform.position.y - par.getRadius()) < par.getRadius() * 2)
                    {
                        waterVolume = (1 / 3f) * Mathf.PI * Mathf.Pow(waterLevel - (obj.transform.position.y - par.getRadius()), 2) * (3 * par.getRadius() - ( waterLevel - (obj.transform.position.y - par.getRadius())));
                    }
                    else
                    {
                        waterVolume = (3 / 4f) * Mathf.PI * Mathf.Pow(par.getRadius(), 3);
                    }
                    
                        //volume * (waterLevel - (obj.transform.position.y - par.getRadius())) / (2 * par.getRadius());
                    //calculate the buoyancy force
                    float buoyancyForce = waterVolume * waterDensity * par.getGravity().y;
                    Debug.Log(waterVolume);
                    //apply the buoyancy force to the object
                    //Debug.Log(buoyancyForce / par.getMass() * duration);
                    par.addForce(new Vector3(0, -(buoyancyForce / par.getMass() * duration), 0));
                    //Debug.Log(buoyancyForce / par.getMass());
                    //Debug.Break();
                }
                break;
            case shapeType.cube:
                if (obj.transform.position.y - par.getRadius() < waterLevel)
                {

                    //calculate the volume of the object
                    //float volume = Mathf.Pow()
                    //calculate the volume of the water that the object is submerged in
                    float waterVolume;

                    if (waterLevel - (obj.transform.position.y - par.getRadius()) > par.getRadius() * 2)
                        waterVolume = Mathf.Pow(par.getRadius() * 2, 2) * (par.getRadius() * 2);
                    else
                        waterVolume = Mathf.Pow(par.getRadius() * 2, 2) * (waterLevel - (obj.transform.position.y - par.getRadius()));

                    
                    //calculate the buoyancy force
                    float buoyancyForce = waterVolume * waterDensity * par.getGravity().y;
                    //apply the buoyancy force to the object
                    //Debug.Log(buoyancyForce / par.getMass() * duration);
                    par.addForce(new Vector3(0, -(buoyancyForce / par.getMass() * duration), 0));
                    //Debug.Log(buoyancyForce / par.getMass());
                    //Debug.Break();
                }
                break;
            case shapeType.cylinder:

                if (obj.transform.position.y - par.getRadius() < waterLevel)
                {

                    //calculate the volume of the object
                    float waterVolume;

                    if (waterLevel - (obj.transform.position.y - par.getRadius()) > par.getRadius() * 2)
                        waterVolume = Mathf.PI * Mathf.Pow(par.getRadius(), 2) * (par.getRadius() * 2);
                    else
                        waterVolume = Mathf.PI * Mathf.Pow(par.getRadius(), 2) * (waterLevel - (obj.transform.position.y - par.getRadius()));
                    
                    //calculate the buoyancy force
                    float buoyancyForce = waterVolume * waterDensity * par.getGravity().y;
                    //apply the buoyancy force to the object
                    //Debug.Log(buoyancyForce / par.getMass() * duration);
                    par.addForce(new Vector3(0, -(buoyancyForce / par.getMass() * duration), 0));
                    //Debug.Log(buoyancyForce / par.getMass());
                    //Debug.Break();
                }

                break;



        }
        

    }

}
