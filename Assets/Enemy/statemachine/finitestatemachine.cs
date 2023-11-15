using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finitestatemachine
{
    public state currentstate 
    { 
        get;
        private set; 
    }
        public void Initialized(state startingstate)
    {

        currentstate = startingstate;
        currentstate.Enter();

    
    }

    public void changestate(state newstate)
    {
        currentstate.Exit();
        currentstate = newstate;
        currentstate.Enter();
    }
}