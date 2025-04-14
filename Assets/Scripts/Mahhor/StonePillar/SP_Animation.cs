using UnityEngine;
using System.Collections;
using UnityEditor;

public class SP_Animation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private SP_Invoke invokeStonePillar;
    public float idleTime = 1f;
    private float animationTime;
    

    private void Start()
    {
        invokeStonePillar = GameObject.Find("Mahhor")?.GetComponent<SP_Invoke>();

        animator = GetComponent<Animator>();
        

        if (animator == null || invokeStonePillar == null)
        {
        Debug.Log("Animator == null");
            return;
        }
        else
        {
        Debug.Log("Começando corrotina da animação");
            StartCoroutine(WaitForCreateAnimationEnd());
        }
    }

    private IEnumerator WaitForCreateAnimationEnd()
    {
        animator.SetBool("create", true);
        yield return new WaitForSeconds(idleTime);

        animator.SetBool("create", false);
        animator.SetBool("idle", true);

        yield return new WaitForSeconds(idleTime); 

        animator.SetBool("idle", false);
        animator.SetBool("destroy", true);

        yield return new WaitForSeconds(0.03f);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Destroy"))
        {
            animationTime = stateInfo.length;
            yield return new WaitForSeconds(animationTime);
            invokeStonePillar.StartDestroyPillar();
        }
        
    }
}
