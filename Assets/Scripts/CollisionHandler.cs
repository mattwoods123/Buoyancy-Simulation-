using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollisionHandler : MonoBehaviour
{
    public TextMeshProUGUI collText;

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkForCollision();

  
    }

    void checkForCollision()
    {
        if(holder.childCount >= 1)
        {

            for (int i = 0; i <holder.childCount; i++)
            {
                GameObject p = holder.GetChild(i).gameObject;
                if (checkPlaneOverLap(plane, normal1, holder.GetChild(i).gameObject))
                {
                    

                    
                    
                }
                else if (checkPlaneOverLap(plane2, normal2, holder.GetChild(i).gameObject))
                {
                    // resolve penitration
                   
                }
            }

            for(int i = 0; i < holder.childCount; i++)
            {
                for (int j = 0; j < holder.childCount; j++)
                {
                    if(i != j && checkOverlap(holder.GetChild(i).gameObject, holder.GetChild(j).gameObject))
                    {
                        Debug.Log("Collision");
                        // TO DO Displace via mass 
                        GameObject p1 = holder.GetChild(i).gameObject;
                        GameObject p2 = holder.GetChild(j).gameObject;

                        float radiusDistance = p1.GetComponent<Particle2D>().getRadius() + p2.GetComponent<Particle2D>().getRadius();

                        Vector2 v1 = p1.transform.position;
                        Vector2 v2 = p2.transform.position;

                        Vector2 distance = v1 - v2;

                        float mag = distance.magnitude;

                        float intersection = radiusDistance - mag;

                        float totalInverserMass = (p1.GetComponent<Particle2D>().getInvMass() + p2.GetComponent<Particle2D>().getInvMass());
                        float intersectionMass = intersection/totalInverserMass;

                        float p1MassScaler = intersectionMass * p1.GetComponent<Particle2D>().getMass();
                        float p2MassScaler = intersectionMass * p2.GetComponent<Particle2D>().getMass();

                        Vector2 p1displace = -(v2 - v1).normalized * p1MassScaler; 
                        Vector2 p2displace = -(v1 - v2).normalized * p2MassScaler;

                        p1.transform.Translate(p1displace);
                        p2.transform.Translate(p2displace);



                        // Change Velocity with mass and resolution

                        //float normal = distance / distance.magnitude;

                        float seperationVel = Vector2.Dot(p1.GetComponent<Particle2D>().Velocity, p2.GetComponent<Particle2D>().Velocity);// maybe normilized
                        Debug.Log(seperationVel);
                        float newSeperationVel = -seperationVel * p1.GetComponent<Particle2D>().getRestitution();
                        Debug.Log(newSeperationVel);
                        float deltaVelocity = newSeperationVel - seperationVel;
                        Debug.Log(deltaVelocity);
                        float impulseForce = deltaVelocity / totalInverserMass;
                        Debug.Log(impulseForce);
                        Vector2 newV1 = (distance).normalized * (impulseForce) * p1.GetComponent<Particle2D>().getInvMass();//maybe not normilized

                        p1.GetComponent<Particle2D>().Velocity = -newV1;

                        float seperationVel2 = Vector2.Dot(p1.GetComponent<Particle2D>().Velocity, p2.GetComponent<Particle2D>().Velocity);// maybe normilized
                        float newSeperationVel2 = -seperationVel * p2.GetComponent<Particle2D>().getRestitution();
                        float deltaVelocity2 = newSeperationVel - seperationVel;
                        float impulseForce2 = deltaVelocity / totalInverserMass;
                        Vector2 newV2 = -((distance).normalized) * (impulseForce) * p2.GetComponent<Particle2D>().getInvMass();//maybe not normilized

                        p2.GetComponent<Particle2D>().Velocity = -newV2;
                        


                    }
                }
            }

        }
    }

    
    

    bool checkOverlap(GameObject p1, GameObject p2)
    {
        Vector2 v1 = p1.transform.position;
        Vector2 v2 = p2.transform.position;
        Vector2 vd = v1 - v2;
        float mag = Mathf.Abs(vd.magnitude);
        
        float triggerDistance = p1.GetComponent<Particle2D>().getRadius() + p2.GetComponent<Particle2D>().getRadius();

        if (mag < triggerDistance)
        {
            addCollision();
            return true;
        }
        return false;
    }

    void addCollision()
    {
        collisions++;
        collText.text = "Collisions: " + collisions;
    }
    void ClearCollision()
    {
        collisions = 0;
        collText.text = "Collisions: " + collisions;
    }
    */
}
