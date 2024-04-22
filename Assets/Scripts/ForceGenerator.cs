using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGenerator
{
    public virtual void updateForce(Particle2D par, float duration) { }
    public virtual void updateForce(Particle3D par, float duration) { }
}
