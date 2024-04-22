using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2D : MonoBehaviour
{
    [Header("Particle 2D Attributes")]
    //Transform tran;
    [SerializeField] float mass = 1;
    [SerializeField] float restitution = 1;
    public Vector2 Velocity;
    public Vector2 acceleration = new Vector2(0, 0);
    [SerializeField] Vector2 accumulativeForce;
    [SerializeField] float dampingConstant = 0.1f;
    [SerializeField] Vector2 gravityScaler = new Vector2(0,-9.8f);
    [SerializeField] bool useGravity = false;
    
    [SerializeField] float amplitude = 0.5f;
    [SerializeField] float selfDestruct = 10;
    [SerializeField]float radius = 0.5f;
    public DragForce myDrag;
    public SpringForce mySpring;
    public bool useDrag = false;
   
    public bool isSpring = false;
    public bool isSpringFixed = true;
    public bool isAttracting = true;

    public GameObject springAnchor;




    private void Awake()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        // tran = transform;
        //Debug.Log(gameObject.GetInstanceID());    

        accumulativeForce = new Vector2(0, 0);

        if (useGravity) { }
            //myRegistry.add(this, new GravityForce(gravityScaler));

        if (useDrag)
        {
            myDrag = new DragForce(dampingConstant, dampingConstant);
        }
        if (isSpring)
        {
            if(isSpringFixed)
                springAnchor = GameObject.Find("SpringJoint");

            if (isAttracting)
                mySpring = new SpringForce(springAnchor, 12.5f, 4);
            else 
                mySpring = new SpringForce(springAnchor, 12.5f, 4);
        }
        dampingConstant = 1;

        //accumulativeForce = new Vector2(0, 0);

        //myRegistry.add(this, new DragForce(dampingConstant,dampingConstant));
        // myRegistry.add(this, new SpringForce());
        Destroy(gameObject, selfDestruct);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Integrator.Integrate(this);
    }

    public void setVelocity(Vector2 vec)
    {
        Velocity = vec;
    }
    public void addForce(Vector2 force)
    {
       
        accumulativeForce += force;
    }
    public Vector2 getForce()
    {
        return accumulativeForce;
    }
    public void zeroForce()
    {
        accumulativeForce = Vector2.zero;
    }
    public void addToReg(ref GameObject g, ForceGenerator fg)
    {
       // myRegistry.add(g.GetComponent<Particle2D>(), fg);

    }
    public float getDampening()
    {
        return dampingConstant;
    }
    public float getRadius()
    {
        return radius;
    }
    public Vector2 getGravity()
    {
        return gravityScaler;
    }
    public bool getUseGravity()
    {
        return useGravity;
    }
    public float getMass()
    {
        return mass;
    }
    public float getInvMass()
    {
        return 1 / mass;
    }
    public float getRestitution()
    {
        return restitution;
    }
}
