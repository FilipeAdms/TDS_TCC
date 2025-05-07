using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MahhorMoveState : MahhorState
{
    public MahhorMoveState(MahhorStateMachine unit) : base(unit) { } // Construtor que recebe a unidade
    public EntityComponent entityComponent;

    int patrolIndex;

    public override void Enter()
    {
        // Inicializa o EntityComponent
        entityComponent = unit.GetComponent<EntityComponent>();        

        patrolIndex = Random.Range(0, 4);
        Debug.Log("Patrol Index: " + patrolIndex);
        Debug.Log("Patrol 1 position: " + unit.MahhorController.patrolPoints[0].transform.position);
        Debug.Log("Patrol 2 position: " + unit.MahhorController.patrolPoints[1].transform.position);
        Debug.Log("Patrol 3 position: " + unit.MahhorController.patrolPoints[2].transform.position);
        Debug.Log("Patrol 4 position: " + unit.MahhorController.patrolPoints[3].transform.position);
    }

    public override void Tick()
    {
        
        if (unit.Transforms.position == unit.MahhorController.patrolPoints[patrolIndex].transform.position)
        {
            Debug.Log("Indo para IdleState");

            unit.ChangeState<MahhorIdleState>();
        } else
        {
            MoveTo(unit.MahhorController.patrolPoints[patrolIndex].transform.position);
            Debug.Log("Moving to " + unit.MahhorController.patrolPoints[patrolIndex].transform.position);
        }
    }

    public override void Exit()
    {
        Debug.Log("Hora de garantir que estou parado");

        unit.Rigidbody2d.velocity = Vector2.zero;
    }

    private void MoveTo(Vector2 targetPosition)
    {


        unit.Transforms.position = Vector2.MoveTowards(unit.Transforms.position, targetPosition, unit.Status.currentMoveSpeed * Time.deltaTime * 10f);
    }
}
