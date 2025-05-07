using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahhorMoveState : MahhorState
{
    public MahhorMoveState(MahhorStateMachine unit) : base(unit) { } // Construtor que recebe a unidade
    public EntityComponent entityComponent;



    public override void Enter()
    {
        // Inicializa o EntityComponent
        entityComponent = unit.GetComponent<EntityComponent>();
    }

    public override void Tick()
    {
        
    }

    public override void Exit()
    {
        entityComponent.Rigidbody.velocity = Vector2.zero;
    }

    private void Move(Directions currentDirection)
    {

    }
}
