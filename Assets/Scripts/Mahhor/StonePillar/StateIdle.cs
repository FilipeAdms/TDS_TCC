using System.Collections;
using UnityEngine;

public class StateIdle : State
{
    private float idleTime = 1f; // Tempo que o pilar fica ativo

    public StateIdle(MonoBehaviour owner, StateMachine stateMachine, Animator animator)
        : base(owner, stateMachine, animator) { }

    public override void Enter()
    {
        // Toca a animação de idle 
        animator.Play("Idle");

        // Espera o tempo definido antes de trocar para Destroy
        owner.StartCoroutine(WaitBeforeDestroy());
    }

    private IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForSeconds(idleTime);
        stateMachine.ChangeState(new StateDestroy(owner, stateMachine, animator));
    }
}
