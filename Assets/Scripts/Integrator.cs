using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Integrator
{
    public static void Integrate(Particle2D par) {
        //par.zeroForce();
        //par.myRegistry.updateForces(Time.deltaTime);

        //par.gameObject.transform.position += (Vector3)par.getForce() * Time.deltaTime;

        if (par.useDrag)
        {
            //par.myDrag.updateForce(par, Time.deltaTime);
        }
        if (par.isSpring)
        {
            par.mySpring.updateForce(par, Time.deltaTime);
        }
        Vector2 resultFor = par.acceleration;
        resultFor += par.getForce() * par.getInvMass();

        par.gameObject.transform.position += (Vector3)par.Velocity * Time.deltaTime;

        par.Velocity = par.Velocity * par.getDampening() + resultFor * Time.deltaTime;

        par.zeroForce();

        //Debug.Log(par.getForce());

    }

    public static void Integrate(Particle3D par)
    {

        //par.myRegistry.updateForces(Time.deltaTime);
 
        par.wf.updateForce(par, Time.deltaTime);
        //Debug.Break();
        
        par.addForce(par.getGravity() * Time.deltaTime);

        Vector3 resultFor = par.acceleration;

        resultFor += par.getForce() * par.getInvMass();

        par.Velocity = par.Velocity * par.getDampening() + resultFor;

        par.gameObject.transform.position += par.Velocity * Time.deltaTime;

        par.zeroForce();

    }
}
