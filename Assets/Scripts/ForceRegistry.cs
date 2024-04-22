using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceRegistry
{
    List<ForceNode> registry = new List<ForceNode>();
    public ForceRegistry()
    {
        registry = new List<ForceNode>();
    }

    public struct ForceNode{
        public Particle2D par;
        public ForceGenerator fg;

        public ForceNode(Particle2D par, ForceGenerator fg)
        {
            this.par = par;
            this.fg = fg;
        }

    }

    //methods 
    public void add(Particle2D par, ForceGenerator fg)
    {
        ForceNode force = new ForceNode(par, fg);
        registry.Add(force);
        //Debug.Log("did add");
        //Debug.Log(registry[0].fg);
    }

    public void remove(Particle2D par, ForceGenerator fg)
    {
        if (registry.Count != 0)
        {
            for (int i = 0; i < registry.Count; i++)
            {
                if (par == registry[i].par && fg == registry[i].fg)
                {
                    registry.RemoveAt(i);
                }
            }
        }
    }

    public void updateForces(float duration)
    {
        
        //Debug.Log(registry[0].fg);
        if (registry.Count > 0)
        {
            Debug.Log(registry.Count);
            for (int i = 0; i < registry.Count; i++)
            {
                registry[i].fg.updateForce(registry[i].par, duration);
                //Debug.Log("Running");
                //Debug.Log(registry[i].fg);
            }
        }
    }
}
