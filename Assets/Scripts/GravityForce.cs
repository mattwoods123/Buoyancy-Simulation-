using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForce : ForceGenerator
{
    Vector2 gravity;
    public GravityForce(Vector2 gravity)
    {
        this.gravity = gravity;
    }

    public override void updateForce(Particle2D par, float duration) {
        par.addForce(gravity * par.getMass() * duration);
    }
}
