using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KenzenRunningState : State
{
    public KenzenRunningState(MonoBehaviour owner, StateMachine stateMachine, Animator animator)
        : base(owner, stateMachine, animator) { }
    public override void Enter()
    {
        animator.Play("KenzenRunning");
    }
    public override void Update() { } 
    public override void Exit() { }  
}
