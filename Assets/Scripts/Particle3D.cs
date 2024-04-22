using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle3D : MonoBehaviour
{
    public CollisionDisplay collDis;
    [Header("Particle 2D Attributes")]
    //Transform tran;
    [SerializeField] float mass = 1;
    [SerializeField] float restitution = 1;
    public Vector3 Velocity;
    public Vector3 acceleration = new Vector3(0, 0, 0); 
    [SerializeField] Vector3 accumulativeForce;
    [SerializeField] float dampingConstant = 0.1f;
    [SerializeField] Vector3 gravityScaler = new Vector3(0, -9.8f, 0);
    [SerializeField] bool useGravity = false; 
    [SerializeField] float amplitude = 0.5f;
    [SerializeField] float selfDestruct = 10;
    [SerializeField] float radius = 0.5f;
    public DragForce myDrag;
    public bool useDrag = false;


    public WaterForce wf;

    public shapeType myShape;

    public float wd;


    public GameObject water;


    public List<GameObject> colCheck;




    private void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        // tran = transform;
        //Debug.Log(gameObject.GetInstanceID());    

        //collDis = GameObject.Find("Canvas").GetComponent<CollisionDisplay>();

        wf = new WaterForce(water, wd, myShape);

        radius = gameObject.transform.localScale.x / 2;

        colCheck = new List<GameObject>();

        holder = GameObject.Find("ProjectileHolder");

        planes = GameObject.Find("PlaneHolder").transform;

        accumulativeForce = new Vector3(0, 0, 0);

        if (useGravity) {

        }
        //myRegistry.add(this, new GravityForce(gravityScaler));

        if (useDrag)
        {
           // myDrag = new DragForce(dampingConstant, dampingConstant);
        }

        //dampingConstant = 1;

        //accumulativeForce = new Vector2(0, 0);

        //myRegistry.add(this, new DragForce(dampingConstant,dampingConstant));
        // myRegistry.add(this, new SpringForce());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        collision();

        Integrator.Integrate(this);

        //Destroy(gameObject, selfDestruct);
    }

    public void setVelocity(Vector3 vec)
    {
        Velocity = vec;
    }
    public void addForce(Vector3 force)
    {

        accumulativeForce += force;
    }
    public Vector3 getForce()
    {
        return accumulativeForce;
    }
    public void zeroForce()
    {
        accumulativeForce = Vector3.zero;
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
    public Vector3 getGravity()
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

    // 3D Collision

    public GameObject holder;

    public GameObject colObj;

    Vector3 objPos, otherPos, otherVel, dis, dir, normal, objNewVel, otherNewVel = Vector3.zero;

    float penetration, bounce, velDelta, mag, otherMass, otherRadius, seperationVel, TotalInvMass, ScaledInvMass, otherInverseMass, magModify  = 0;

    public Transform planes;
    public Vector3 normal1;

    void collision()
    {
        
        //for (int i = 0; i < holder.transform.childCount; i++)
        //{
        //    if (holder.transform.childCount > 1 && checkOverlap(gameObject, holder.transform.GetChild(i).gameObject)) 
        //        colCheck.Add(holder.transform.GetChild(i).gameObject);
        //}

        for(int i = 0; i < colCheck.Count; i++)
        {
            //Debug.Break();
            colObj = colCheck[i];
            if (checkOverlap(gameObject, colObj))
            {
                penetrationOutcome();
                velecityOutcome();
                //collDis.addScore();
            }
        }

        for(int i = 0; i < planes.childCount; i++)
        {
            GameObject plane = planes.GetChild(i).gameObject;
            Vector3 normal = plane.GetComponent<PlaneInfo>().normal;

            if (checkPlaneOverLap(plane, normal, gameObject))
            {
                resolvePlaneCol(plane, normal);
            }

        }

    }
    public bool checkOverlap(GameObject p1, GameObject p2)
    {
        Vector3 v1 = p1.transform.position;
        Vector3 v2 = p2.transform.position;
        Vector3 vd = v1 - v2;
        float mag = Mathf.Abs(vd.magnitude);

        float triggerDistance = p1.GetComponent<Particle3D>().getRadius() + p2.GetComponent<Particle3D>().getRadius();

        if (mag < triggerDistance && p1 != p2)
        {
            return true;
        }
        return false;
    }

    void penetrationOutcome()
    {
        objPos = gameObject.transform.position;
        otherPos = colObj.transform.position;

        otherMass = colObj.GetComponent<Particle3D>().getMass();

        otherVel = colObj.GetComponent<Particle3D>().Velocity;

        otherRadius = colObj.GetComponent<Particle3D>().getRadius();

        otherInverseMass = colObj.GetComponent<Particle3D>().getInvMass();

        TotalInvMass = getInvMass() + otherInverseMass;

        dis = objPos - otherPos;
        dir = dis.normalized;
        mag = dis.magnitude;
        normal = dis / mag;

        penetration = (getRadius() + otherRadius) - mag;
        ScaledInvMass = penetration / TotalInvMass;
        float objChange = ScaledInvMass * getInvMass();
        float otherChange = ScaledInvMass * otherInverseMass;

        gameObject.transform.position += normal * objChange;
        colObj.transform.position += -normal * otherChange;
    }
    void velecityOutcome()
    {
        seperationVel = Vector3.Dot(Velocity - otherVel, -dir);
        bounce = -seperationVel * restitution;
        velDelta = bounce - seperationVel;
        magModify = velDelta / TotalInvMass;

        float objMag = magModify * getInvMass();
        float otherMag = magModify * otherInverseMass;

        objNewVel = -dir * objMag;
        otherNewVel = dir * otherMag;

        Velocity += objNewVel;
        colObj.GetComponent<Particle3D>().Velocity += otherNewVel;
    }
  

    void resolvePlaneCol(GameObject plane, Vector3 normal)
    {
        Vector3 plainPos = plane.transform.position;
        float distanceFrom = Mathf.Abs((normal.x * (plainPos.x - transform.position.x) + normal.y * (plainPos.y - transform.position.y) + normal.z * (plainPos.z - transform.position.z)));

        float planePen = Mathf.Abs(getRadius() - distanceFrom);

        transform.position += normal * planePen;

        //Bouncing off 

        float PseperationVec = Vector3.Dot(Velocity, normal);
        //Debug.Log(seperationVec);
        float PnewSeperation = -PseperationVec * getRestitution();
        //Debug.Log(newSeperation);
        float PdeltaVel = (PnewSeperation - PseperationVec);
        PdeltaVel /= getInvMass();
        //Debug.Log(deltaVel);
        float Pmag = PdeltaVel * getInvMass();
        //Debug.Log(normal * mag);
        Velocity += normal * Pmag;
    }
    public void resolveCubeCol(Vector3 plainPos, Vector3 normal)
    {
        float distanceFrom = Mathf.Abs((normal.x * (plainPos.x - transform.position.x) + normal.y * (plainPos.y - transform.position.y) + normal.z * (plainPos.z - transform.position.z)));

        float planePen = Mathf.Abs(getRadius() - distanceFrom);

        transform.position += normal * planePen;

        //Bouncing off 

        float PseperationVec = Vector3.Dot(Velocity, normal);
        //Debug.Log(seperationVec);
        float PnewSeperation = -PseperationVec * getRestitution();
        //Debug.Log(newSeperation);
        float PdeltaVel = (PnewSeperation - PseperationVec);
        PdeltaVel /= getInvMass();
        //Debug.Log(deltaVel);
        float Pmag = PdeltaVel * getInvMass();
        //Debug.Log(normal * mag);
        Velocity += normal * Pmag;
    }

    bool checkPlaneOverLap(GameObject plain, Vector3 normal, GameObject p)
    {

        Vector3 plainPos = plain.transform.position;
        Vector3 pPos = p.transform.position;

        float plainNorm = Mathf.Abs(normal.x * (plainPos.x - pPos.x) + normal.y * (plainPos.y - pPos.y) + normal.z * (plainPos.z - pPos.z));
        //Debug.Log(plainNorm);
        if (p.GetComponent<Particle3D>().getRadius() > plainNorm)
        {
            return true;
        }

        return false;
    }

}
