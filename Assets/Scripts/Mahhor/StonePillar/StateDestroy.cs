using System.Collections;
using UnityEngine;

public class StateDestroy : State
{
    public StateDestroy(MonoBehaviour owner, StateMachine stateMachine, Animator animator)
        : base(owner, stateMachine, animator) { }

    public override void Enter()
    {
        // Toca a anima��o de destrui��o
        animator.Play("Destroy");

        // Espera a anima��o terminar antes de destruir o objeto
        owner.StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(GetAnimationLength("Destroy"));

        // Destroi o pilar ap�s a anima��o acabar
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
