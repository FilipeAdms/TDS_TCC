using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MahhorMoveState : MahhorState
{
    public MahhorMoveState(MahhorStateMachine unit) : base(unit) { } // Construtor que recebe a unidade

    private int patrolIndex; // �ndice do ponto de patrulha
    private Vector3 direction; // Dire��o que est� o ponto de patrulha

    public override void Enter()
    {
        patrolIndex = Random.Range(0, 4);
        if (unit.MahhorController.currentTransformation == MahhorTransformation.Default)
        {
            // Dire��o do ponto de patrulha
            direction = unit.MahhorController.patrolPoints[patrolIndex].transform.position - unit.Transforms.position;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x >= 0) // Direita
                {
                    unit.GetAnimator().Play("MahhorWalkingRight", 0);
                }
                else // Esquerda
                {
                    unit.GetAnimator().Play("MahhorWalkingLeft", 0);
                }
            }
            else
            {
                if (direction.y > 0) // Cima
                {
                    unit.GetAnimator().Play("MahhorWalkingUp", 0);
                }
                else // Baixo
                {
                    unit.GetAnimator().Play("MahhorWalkingDown", 0);
                }
            }
        } else if (unit.MahhorController.currentTransformation == MahhorTransformation.Madness)
        {
            unit.GetAnimator().Play("MahhorIdleTransformed", 0);
        }

    }

    public override void Tick()
    {
        if (unit.MahhorController.currentTransformation == MahhorTransformation.Default)
        {
            if (unit.Transforms.position == unit.MahhorController.patrolPoints[patrolIndex].transform.position)
            {
                unit.ChangeState<MahhorIdleState>();
            }
            else
            {
                MoveTo(unit.MahhorController.patrolPoints[patrolIndex].transform.position);
            }
        }
        else if (unit.MahhorController.currentTransformation == MahhorTransformation.Madness)
        {
            if (unit.Transforms.position == unit.MahhorController.patrolPoints[patrolIndex].transform.position)
            {
                unit.ChangeState<MahhorIdleState>();
            }
            else
            {
                TeleportTo(unit.MahhorController.patrolPoints[patrolIndex].transform.position);
            }
        }
    }

    public override void Exit()
    {
        unit.Rigidbody2d.velocity = Vector2.zero;
        unit.StartCoroutine(CanActCooldown());
    }

    private void MoveTo(Vector2 targetPosition)
    {
        unit.Transforms.position = Vector2.MoveTowards(unit.Transforms.position, targetPosition, unit.Status.currentMoveSpeed * Time.deltaTime);
    }

    private void TeleportTo (Vector2 targetPosition)
    {
        unit.Transforms.position = targetPosition;
        unit.Rigidbody2d.velocity = Vector2.zero;
    }

    private IEnumerator CanActCooldown()
    {
        if (unit.MahhorController.currentTransformation == MahhorTransformation.Default)
        {
            yield return new WaitForSeconds(1f);
            unit.MahhorSkillController.canAct = true;
        } else if (unit.MahhorController.currentTransformation == MahhorTransformation.Madness)
        {
            yield return new WaitForSeconds(3f);
            unit.MahhorSkillController.canAct = true;
        }


    }
}
