using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{

    protected UnitStateMachine unit;

    public State(UnitStateMachine unit)
    {
        this.unit = unit;
    }

    // C�digo a ser executado quando o estado � ativado
    public virtual void Enter() { }
    public virtual void Tick() { }
    public virtual void Exit(){ }

}
