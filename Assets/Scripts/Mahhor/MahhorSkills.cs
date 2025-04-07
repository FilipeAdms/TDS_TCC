using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MahhorSkills : MonoBehaviour
{
    private const float stonePillarCD = 10f;  // Tempo de cooldown
    private const float stonePillarRange = 300f;  // Alcance
    private bool stonePillarAvailable = true;  // Verifica se a habilidade est� dispon�vel
    private LayerMask playerMask;  // Layer para detectar o jogador
    private SP_Invoke invokeStonePillar;  // Componente para invocar o pilar
    protected Collider2D[] entities;  // Array de entidades encontradas

    private void Start()
    {
        playerMask = LayerMask.GetMask("Player");  // M�scara para o jogador
        invokeStonePillar = GetComponent<SP_Invoke>();  // Obter o componente SP_Invoke
    }

    private void FixedUpdate()
    {
        // A habilidade � ativada somente se estiver dispon�vel
        if (stonePillarAvailable)
        {
            DetectPlayer();
        }
    }

    // Detecta se o jogador est� dentro do alcance
    protected void DetectPlayer()
    {
        // Detecta todas as entidades dentro 
        entities = Physics2D.OverlapCircleAll(transform.position, stonePillarRange, playerMask);
        if (entities.Length > 0)
        {
            // Ativa a skill ao detectar o jogador
            UseStonePillar();
        }
    }



    // M�todo para usar o StonePillar
    private void UseStonePillar()
    {
        // Inicia o efeito de invocar o pilar e o cooldown
        StartCoroutine(invokeStonePillar.Invoke_StonePillar());
        StartCoroutine(StonePillarResetCooldown());
        stonePillarAvailable = false;  // Marca a habilidade como indispon�vel
    }

    // M�todo que controla o cooldown
    private IEnumerator StonePillarResetCooldown()
    {
        yield return new WaitForSeconds(stonePillarCD);  // Espera o tempo de cooldown
        stonePillarAvailable = true;  // Torna a habilidade dispon�vel novamente
    }

    // M�todo para desenhar o raio de detec��o no editor (Gizmos)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;  // Define a cor do gizmo
        Gizmos.DrawWireSphere(transform.position, stonePillarRange);  // Desenha o raio de detec��o
    }
}
