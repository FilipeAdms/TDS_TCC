using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Licensing;
using UnityEngine;

public class MoveState : State
{
    public MoveState(PlayerStateMachine unit) : base(unit) { } // Construtor que recebe a unidade
    public EntityComponent entityComponent;

    private float vMove;
    private float hMove;


    public override void Enter()
    {
        // Inicializa o EntityComponent
        entityComponent = unit.GetComponent<EntityComponent>();
    }

    public override void Tick()
    {
        if (entityComponent != null)
        {
            // Captura os inputs do jogador
            vMove = Input.GetAxisRaw("Vertical");
            hMove = Input.GetAxisRaw("Horizontal");

            if (vMove != 0 || hMove != 0)
            {
                DirectionUtils.currentDirection = DirectionUtils.UpdateDirection(hMove, vMove);
                Debug.Log("Direção Atual: " + DirectionUtils.currentDirection);
                Move(DirectionUtils.currentDirection);
            }
            else
            {
                entityComponent.Rigidbody.velocity = Vector2.zero;
                unit.ChangeState<IdleState>();
            }
        }
    }

    public override void Exit()
    {
        entityComponent.Rigidbody.velocity = Vector2.zero;
    }

    private void Move(Directions currentDirection)
    {
        // Calcula a direção do movimento
        Vector2 direction = new Vector2(hMove, vMove).normalized;

        // Aplica a velocidade ao Rigidbody
        entityComponent.Rigidbody.velocity = (unit.Status.currentMoveSpeed * direction);

        switch (currentDirection)
            {
                case Directions.Up:
                    unit.GetAnimator().Play("KenzenRunningTop");
                break;
                case Directions.Down:
                    unit.GetAnimator().Play("KenzenRunningDown");
                break;
                case Directions.Left:
                    entityComponent.Transform.rotation = Quaternion.Euler(0, 180, 0);
                    unit.GetAnimator().Play("KenzenRunning");
                break;
                case Directions.Right:
                    entityComponent.Transform.rotation = Quaternion.Euler(0, 0, 0);
                    unit.GetAnimator().Play("KenzenRunning");
                break;
                case Directions.None:
                    unit.GetAnimator().Play("KenzenIdle");
                break;
            }
    }
}
