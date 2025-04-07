using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KenzenRunningDownState : State
{
    public KenzenRunningDownState(MonoBehaviour owner, StateMachine stateMachine, Animator animator)
            : base(owner, stateMachine, animator) { }
    public override void Enter()
    {
        animator.Play("KenzenRunningDown");
    }
    public override void Update() { }
    public override void Exit() { }
}
