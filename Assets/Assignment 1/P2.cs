using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2 : MonoBehaviour
{
    [Header("Particle 2D Attributes")]
    [SerializeField] float mass = 1;
    [SerializeField] Vector2 Velocity = new Vector2(0, 0);
    [SerializeField] Vector2 Acceleration = new Vector2(0, 0);
    [SerializeField] Vector2 AccumulativeForce = new Vector2(0, 0);
    [SerializeField] float DampingConstant = 1;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Ass1Intagrator.Integrate(Velocity, transform.position, Time.deltaTime);
    }

    public void setVelocity(Vector2 vec)
    {
        Velocity = vec;
    }
}
