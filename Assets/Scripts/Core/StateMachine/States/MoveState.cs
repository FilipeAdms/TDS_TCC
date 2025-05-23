using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public MoveState(PlayerStateMachine unit) : base(unit) { } // Construtor que recebe a unidade

    private float vMove;
    private float hMove;


    public override void Enter()
    {
    }

    public override void Tick()
    {
        if (unit.Rigidbody2d != null)
        {
            // Captura os inputs do jogador
            vMove = Input.GetAxisRaw("Vertical");
            hMove = Input.GetAxisRaw("Horizontal");

            if (vMove != 0 || hMove != 0)
            {
                DirectionUtils.currentDirection = DirectionUtils.UpdateDirection(hMove, vMove);
                Move(DirectionUtils.currentDirection);
            }
            else
            {
                unit.Rigidbody2d.velocity = Vector2.zero;
                unit.ChangeState<IdleState>();
            }
        }
    }

    public override void Exit()
    {
        unit.Rigidbody2d.velocity = Vector2.zero;
    }

    private void Move(Directions currentDirection)
    {
        // Calcula a dire��o do movimento
        Vector2 direction = new Vector2(hMove, vMove).normalized;

        // Aplica a velocidade ao Rigidbody
        unit.Rigidbody2d.velocity = (unit.Status.currentMoveSpeed * direction);

        switch (currentDirection)
            {
                case Directions.Up:
                    unit.GetAnimator().Play("KenzenRunningTop");
                break;
                case Directions.Down:
                    unit.GetAnimator().Play("KenzenRunningDown");
                break;
                case Directions.Left:
                    unit.Transforms.rotation = Quaternion.Euler(0, 180, 0);
                    unit.GetAnimator().Play("KenzenRunning");
                break;
                case Directions.Right:
                    unit.Transforms.rotation = Quaternion.Euler(0, 0, 0);
                    unit.GetAnimator().Play("KenzenRunning");
                break;
                case Directions.None:
                    unit.GetAnimator().Play("KenzenIdle");
                break;
            }
    }
}
