using System.Collections;
using UnityEngine;

public class StateCreate : State
{
    public StateCreate(MonoBehaviour owner, StateMachine stateMachine, Animator animator)
        : base(owner, stateMachine, animator) { }

    public override void Enter()
    {
        // Toca a anima��o de cria��o
        animator.Play("Create");

        // Espera a anima��o terminar e muda para o pr�ximo estado
        owner.StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        // Pega o tempo da anima��o e espera
        yield return new WaitForSeconds(GetAnimationLength("Create"));

        // Troca para o pr�ximo estado (Idle)
        stateMachine.ChangeState(new StateIdle(owner, stateMachine, animator));
    }

    public override float GetAnimationLength(string animationName)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
                return clip.length;
        }
        return 1f; // Se n�o achar a anima��o, assume 1 segundo
    }
}
