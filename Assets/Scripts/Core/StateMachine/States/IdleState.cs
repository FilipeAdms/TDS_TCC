using Unity.Burst;
using UnityEngine;

public class IdleState : State
{
    public IdleState(PlayerStateMachine unit) : base(unit) { } // Construtor que recebe a unidade

    public override void Enter()
    {
        // Ativa a animação de Idle
        if (unit.PlayerController.currentElement == ElementType.Default)
        {
            unit.GetAnimator().Play("Idle");
        }
        else if (unit.PlayerController.currentElement == ElementType.Earth)
        {
            unit.GetAnimator().Play("KenzenIdleEarth");
        }
        else if (unit.PlayerController.currentElement == ElementType.Air)
        {
            unit.GetAnimator().Play("KenzenIdleAir");
        }
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
