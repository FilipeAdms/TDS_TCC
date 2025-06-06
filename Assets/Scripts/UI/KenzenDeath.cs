using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KenzenDeath : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer playerRenderer;
    public MahhorSkillController mahhorSkillController;
    public PlayerSkillController playerSkillController;
    public PlayerController playerController;
    public GameObject canvasGameObject;
    public Animator animator;
    public float duracao = 1f;

    public void DeathScene()
    {
        canvasGameObject.SetActive(false);
        playerRenderer.sortingOrder = 100;
        spriteRenderer.sortingOrder = 99;
        if (spriteRenderer != null && animator != null)
            StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        mahhorSkillController.canAct = false; // Desativa as ações do Mahhor
        playerSkillController.canAct = false; // Desativa as ações do jogador
        playerController.canAttack = false; // Desativa os ataques do jogador
        // Começa transparente
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
            yield return null; // Espera o próximo frame
        }

        // Garante alpha no final
        color.a = 1f;
        spriteRenderer.color = color;

        // Toca animação
        animator.Play("KenzenDying");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene("Menu");
    }
}
