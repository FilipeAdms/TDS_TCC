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
        if (unit.Rigidbody2d != null && unit.PlayerSkillController.canAct )
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
        // Calcula a direção do movimento
        Vector2 direction = new Vector2(hMove, vMove).normalized;

        // Aplica a velocidade ao Rigidbody
        unit.Rigidbody2d.velocity = (unit.Status.currentMoveSpeed * direction);

        switch (currentDirection)
            {
                case Directions.Up:
                    if (unit.PlayerController.currentElement == ElementType.Default)
                    {
                        unit.GetAnimator().Play("KenzenRunningTop");
                    }
                    else if (unit.PlayerController.currentElement == ElementType.Air)
                    {
                        unit.GetAnimator().Play("AirKenzenRunningTop");
                    }else if (unit.PlayerController.currentElement == ElementType.Earth)
                    {
                        unit.GetAnimator().Play("EarthKenzenRunningUp");
                    }
                    break;
                case Directions.Down:
                    if (unit.PlayerController.currentElement == ElementType.Default)
                    {
                        unit.GetAnimator().Play("KenzenRunningDown");
                    }
                    else if (unit.PlayerController.currentElement == ElementType.Air)
                    {
                        unit.GetAnimator().Play("AirKenzenRunningDown");
                    }
                    else if (unit.PlayerController.currentElement == ElementType.Earth)
                    {
                        unit.GetAnimator().Play("EarthKenzenRunningDown");
                    }
                break;
                case Directions.Left:
                    unit.Transforms.rotation = Quaternion.Euler(0, 180, 0);
                    if (unit.PlayerController.currentElement == ElementType.Default)
                    {
                        unit.GetAnimator().Play("KenzenRunning");
                    }
                    else if (unit.PlayerController.currentElement == ElementType.Air)
                    {
                        unit.GetAnimator().Play("AirKenzenRunning");
                    }
                    else if (unit.PlayerController.currentElement == ElementType.Earth)
                    {
                        unit.GetAnimator().Play("EarthKenzenRunning");
                    }
                break;
                case Directions.Right:
                    unit.Transforms.rotation = Quaternion.Euler(0, 0, 0);
                    if (unit.PlayerController.currentElement == ElementType.Default)
                    {
                        unit.GetAnimator().Play("KenzenRunning");
                    }
                    else if (unit.PlayerController.currentElement == ElementType.Air)
                    {
                        unit.GetAnimator().Play("AirKenzenRunning");
                    }
                    else if (unit.PlayerController.currentElement == ElementType.Earth)
                    {
                        unit.GetAnimator().Play("EarthKenzenRunning");
                    }
                break;
                case Directions.None:
                    if (unit.PlayerController.currentElement == ElementType.Default)
                    {
                        unit.GetAnimator().Play("KenzenIdle");
                    }
                    else if (unit.PlayerController.currentElement == ElementType.Air)
                    {
                        unit.GetAnimator().Play("KenzenIdleAir");
                    }
                    else if (unit.PlayerController.currentElement == ElementType.Earth)
                    {
                        unit.GetAnimator().Play("EarthKenzenIdle");
                    }
                break;
            }
    }
}
