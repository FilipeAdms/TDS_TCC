using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePillarDamage : MonoBehaviour
{
    [SerializeField] private Transform stonePillarPosition;
    [SerializeField] private Animator stonePillarAnimator;
    private float animationLength;

    // Start is called before the first frame update
    void Start()
    {
        StonePillarAnimation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBars>().TakeDamage(10);
        }
    }
    private void StonePillarAnimation()
    {
        stonePillarAnimator.Play("StonePillar");

        // Pega duração da animação com segurança
        AnimationClip clip = Array.Find(stonePillarAnimator.runtimeAnimatorController.animationClips, c => c.name == "StonePillar");
        animationLength = clip != null ? clip.length : 0.5f; // fallback se não encontrar

        StartCoroutine(WaitForAnimation(animationLength));
    }
    private IEnumerator WaitForAnimation(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }
}
