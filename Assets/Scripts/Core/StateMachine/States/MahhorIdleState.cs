using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahhorIdleState : MahhorState
{
    public MahhorIdleState(MahhorStateMachine unit) : base(unit) { }

    public override void Enter() {
        unit.GetAnimator().Play("Idle");
    }
    public override void Tick() { }
    public override void Exit() { }
}
