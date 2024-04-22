using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragForce : ForceGenerator
{
    float k1;
    float k2;

    public DragForce(float k1, float k2)
    {
        this.k1 = k1;
        this.k2 = k2;
    }

    public override void updateForce(Particle2D par, float duration)
    {
        Vector2 force;
        force = par.Velocity;

        float dragCof = force.magnitude;
        dragCof = k1 * dragCof + k2 * dragCof * dragCof;

        //Debug.Log(dragCof);

        force = force.normalized;
        force *= -dragCof;
        par.Velocity += (force * Time.deltaTime);
    }
}
