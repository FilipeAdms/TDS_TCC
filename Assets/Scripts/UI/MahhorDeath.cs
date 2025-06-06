using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MahhorDeath : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer playerRenderer;
    public MahhorSkillController mahhorSkillController;
    public PlayerSkillController playerSkillController;
    public PlayerController playerController;
    public GameObject canvasGameObject;
    public Animator animator;
    public MahhorSound mahhorSound;
    private float animationLength;
    private string animName = "MahhorDying"; // Nome da anima��o de morte
    public float duracao = 1f;

    public void DeathScene()
    {
        AnimationClip clip = Array.Find(animator.runtimeAnimatorController.animationClips, c => c.name == animName);
        animationLength = clip != null ? clip.length : 0.5f; // fallback se n�o encontrar
        canvasGameObject.SetActive(false);
        playerRenderer.sortingOrder = 100;
        spriteRenderer.sortingOrder = 99;
        if (spriteRenderer != null && animator != null)
            StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        mahhorSkillController.canAct = false; // Desativa as a��es do Mahhor
        playerSkillController.canAct = false; // Desativa as a��es do jogador
        playerController.canAttack = false; // Desativa os ataques do jogador
        // Come�a transparente
        Color color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;

        float tempo = 0f;

        // Faz o fade in suavemente
        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            float alpha = Mathf.Clamp01(tempo / duracao);
            color.a = alpha;
            spriteRenderer.color = color;
            yield return null; // Espera o pr�ximo frame
        }

        // Garante alpha no final
        color.a = 1f;
        spriteRenderer.color = color;

        // Toca anima��o
        animator.Play(animName);
        yield return new WaitForSeconds(animationLength);

        SceneManager.LoadScene("Menu");
    }
}
