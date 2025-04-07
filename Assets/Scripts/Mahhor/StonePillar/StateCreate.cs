using System.Collections;
using UnityEngine;

public class StateCreate : State
{
    public StateCreate(MonoBehaviour owner, StateMachine stateMachine, Animator animator)
        : base(owner, stateMachine, animator) { }

    public override void Enter()
    {
        // Toca a animação de criação
        animator.Play("Create");

        // Espera a animação terminar e muda para o próximo estado
        owner.StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        // Pega o tempo da animação e espera
        yield return new WaitForSeconds(GetAnimationLength("Create"));

        // Troca para o próximo estado (Idle)
        stateMachine.ChangeState(new StateIdle(owner, stateMachine, animator));
    }

    public override float GetAnimationLength(string animationName)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
                return clip.length;
        }
        return 1f; // Se não achar a animação, assume 1 segundo
    }
}
