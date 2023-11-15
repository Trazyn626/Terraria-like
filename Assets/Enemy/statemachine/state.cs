using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class state 
{
    protected finitestatemachine statemachine;
    protected entity entity;
    protected float startime;
    protected string animboolname;
    public state(entity entity, finitestatemachine statemachine, string animboolname) 
    {
        this.entity = entity;
        this.statemachine = statemachine;
        this.animboolname = animboolname;
    }
    public virtual void Enter()
    {

        startime = Time.time;
        entity.anim.SetBool(animboolname,true);
    }
    public virtual void Exit()
    {
        entity.anim.SetBool(animboolname, false);


    }
    public virtual void LogicUpdate()
    {



    }
    public virtual void physicsupdate()
    {



    }
}

