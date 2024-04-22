using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringForce : ForceGenerator
{
    public GameObject target;
    float springConst = 15;
    float length = 5;

    public SpringForce(GameObject otherPar, float springConst, float length)
    {
        this.target = otherPar;
        this.springConst = springConst;
        this.length = length;
    }

    public SpringForce(GameObject otherPar)
    {
        this.target = otherPar;
        this.springConst = 12;
        this.length = 1.5f;
    }

    public override void updateForce(Particle2D par, float duration)
    {
        if (par.isAttracting)
        {
            
            Vector2 force;
            force = par.GetComponentInParent<Transform>().position;
            force -= (Vector2)target.transform.position;

            float mag = force.magnitude;
            if (mag > length)
            {
                mag = Mathf.Abs(mag - length);
                mag *= springConst;

                force = force.normalized;
                force *= -mag;
                par.addForce(force);
            }

        
            Debug.Log(force);
        }
        else
        {
            Vector2 force;
            force = par.GetComponentInParent<Transform>().position;
            force -= (Vector2)target.transform.position;

            float mag = force.magnitude;

            //mag = length / mag;
            //mag /= springConst;
            if(mag < length)
            {
                //Debug.Break();
                mag = Mathf.Abs(length - mag);
                mag *= springConst;
                force = force.normalized;
                force *= -mag;
                par.addForce(-force);
                Debug.Log(force);
            }
            else
            {
                par.addForce(Vector2.zero);
            }
        }
        
    }

}
