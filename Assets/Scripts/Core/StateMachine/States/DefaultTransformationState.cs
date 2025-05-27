using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTransformationState : State
{
    public DefaultTransformationState(PlayerStateMachine unit) : base(unit)
    {
    }

    public override void Enter()
    {
        unit.PlayerController.currentElement = ElementType.Default; // Define o elemento atual como Padrão

    }
    public override void Tick()
    {

    }
    public override void Exit()
    {
        unit.ChangeState<IdleState>();

    }
}
