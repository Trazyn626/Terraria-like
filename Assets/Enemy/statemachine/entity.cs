using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entity : MonoBehaviour
{
    public finitestatemachine statemachine;
    public int facingdirction { get; private set; } 
    public Rigidbody2D entityrb { get;private set; }
    public Animator anim{ get;private set; }
    public GameObject aliveGO { get; private set; }
    private Vector2 velocityworkspace;

    public virtual void Start()
    {

        aliveGO = transform.Find("alive").gameObject;
        entityrb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();

        statemachine = new finitestatemachine();

    }
    public virtual void Update()
    {
        statemachine.currentstate.LogicUpdate();
    
    
    }
    public virtual void FixedUpdate ()
    {

        statemachine.currentstate.physicsupdate();
    }
    public virtual void Setvelocity(float velocity)
    {

        velocityworkspace.Set(facingdirction * velocity, entityrb.velocity.y);
        entityrb.velocity = velocityworkspace;
    }
}
