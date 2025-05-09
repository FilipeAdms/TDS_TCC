using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalagmitDamage : MonoBehaviour
{

    [SerializeField] private Transform stalagmitePosition;
    [SerializeField] private Animator stalagmiteAnimator;
    private float animationLength;

    // Start is called before the first frame update
    void Start()
    {
        StalagmiteAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBars>().TakeDamage(4);
        }
    }
    private void StalagmiteAnimation()
    {
        stalagmiteAnimator.Play("Stalagmite");

        // Pega duração da animação com segurança
        AnimationClip clip = Array.Find(stalagmiteAnimator.runtimeAnimatorController.animationClips, c => c.name == "Stalagmite");
        animationLength = clip != null ? clip.length : 0.5f; // fallback se não encontrar

        StartCoroutine(WaitForAnimation(animationLength));
    }
    private IEnumerator WaitForAnimation(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }
}
