using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KenzenRunningTopState : State
{
    public KenzenRunningTopState(MonoBehaviour owner, StateMachine stateMachine, Animator animator)
            : base(owner, stateMachine, animator) { }
    public override void Enter()
    {
        animator.Play("KenzenRunningTop");
    }
    public override void Update() { }
    public override void Exit() { }
}
