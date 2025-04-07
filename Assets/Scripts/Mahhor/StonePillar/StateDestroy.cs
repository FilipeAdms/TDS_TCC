using System.Collections;
using UnityEngine;

public class StateDestroy : State
{
    public StateDestroy(MonoBehaviour owner, StateMachine stateMachine, Animator animator)
        : base(owner, stateMachine, animator) { }

    public override void Enter()
    {
        // Toca a animação de destruição
        animator.Play("Destroy");

        // Espera a animação terminar antes de destruir o objeto
        owner.StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(GetAnimationLength("Destroy"));

        // Destroi o pilar após a animação acabar
        GameObject.Destroy(owner.gameObject);
    }

    public override float GetAnimationLength(string animationName)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return 1f;
    }
}
