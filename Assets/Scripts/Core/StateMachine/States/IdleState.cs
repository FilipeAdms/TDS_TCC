using Unity.Burst;
using UnityEngine;

public class IdleState : State
{
    public IdleState(UnitStateMachine unit) : base(unit) { } // Construtor que recebe a unidade

    public override void Enter()
    {
        // Ativa a animação de Idle
        unit.GetAnimator().Play("Idle");

    }

    public override void Exit()
    {

    }


    public override void Tick()
    {
        float vMove = Input.GetAxis("Vertical");
        float hMove = Input.GetAxis("Horizontal");
        if (vMove != 0 || hMove != 0)
        {
            unit.ChangeState<MoveState>();
        }
    }
}
