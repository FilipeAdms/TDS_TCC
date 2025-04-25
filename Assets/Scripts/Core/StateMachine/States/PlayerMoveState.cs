using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Licensing;
using UnityEngine;

public class PlayerMoveState : State
{
    public EntityComponent entityComponent;

    private float vMove;
    private float hMove;

    public PlayerMoveState(UnitStateMachine unit) : base(unit) { } // Construtor que recebe a unidade

    public override void Enter()
    {
        // Inicializa o EntityComponent
        entityComponent = unit.GetComponent<EntityComponent>();
        if (entityComponent == null)
        {
            Debug.LogError("EntityComponent não encontrado no GameObject!");
        }
        else
        {
            Debug.Log("Entrando no estado PlayerMoveState.");
        }
    }

    public override void Tick()
    {
        if (entityComponent != null)
        {
            // Captura os inputs do jogador
            vMove = Input.GetAxisRaw("Vertical");
            hMove = Input.GetAxisRaw("Horizontal");

            Debug.Log($"PlayerMoveState - Inputs: Vertical = {vMove}, Horizontal = {hMove}");

            if (vMove != 0 || hMove != 0)
            {
                Debug.Log("PlayerMoveState - Jogador está se movendo.");
                Move();
            }
            else
            {
                entityComponent.Rigidbody.velocity = Vector2.zero;
                Debug.Log("PlayerMoveState - Jogador parou de se mover. Mudando para PlayerIdleState.");
                unit.ChangeState<PlayerIdleState>();
            }
        }
    }

    public override void Exit()
    {
        Debug.Log("Saindo do estado PlayerMoveState.");
    }

    private void Move()
    {
        // Calcula a direção do movimento
        Vector2 direction = new Vector2(hMove, vMove).normalized;

        // Aplica a velocidade ao Rigidbody
        entityComponent.Rigidbody.velocity = (unit.status.MoveSpeed * direction);
        Debug.Log($"PlayerMoveState - Movendo na direção: {direction}, Velocidade: {unit.status.MoveSpeed}");

        // Gerencia as animações baseadas no movimento
        if (vMove > 0)
        {
            unit.GetAnimator().Play("RunningTop");
            Debug.Log("PlayerMoveState - Animação: RunningTop");
        }
        else if (vMove < 0)
        {
            unit.GetAnimator().Play("RunningDown");
            Debug.Log("PlayerMoveState - Animação: RunningDown");
        }
        else if (hMove > 0)
        {
            unit.GetAnimator().Play("Running");
            entityComponent.Transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("PlayerMoveState - Animação: Running (Direita)");
        }
        else if (hMove < 0)
        {
            unit.GetAnimator().Play("Running");
            entityComponent.Transform.rotation = Quaternion.Euler(0, 180, 0);
            Debug.Log("PlayerMoveState - Animação: Running (Esquerda)");
        }
    }
}
