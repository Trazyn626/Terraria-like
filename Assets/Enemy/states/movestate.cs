using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movestate : state
{
    protected D_movestate statedata;
    public movestate(entity entity, finitestatemachine statemachine, string animboolname,D_movestate statedata ) : base(entity, statemachine, animboolname)
    {
        this.statedata = statedata;
    }
    public override void Enter()
    {
        base.Enter();
        entity.Setvelocity(statedata.movementspeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void physicsupdate()
    {
        base.physicsupdate();
    }
}
