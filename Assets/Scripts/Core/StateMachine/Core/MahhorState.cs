using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MahhorState
{
    protected MahhorStateMachine unit;

    public MahhorState(MahhorStateMachine unit)
    {
        this.unit = unit;
    }

    // Código a ser executado quando o estado é ativado
    public virtual void Enter() { }
    public virtual void Tick() { }
    public virtual void Exit() { }

}
