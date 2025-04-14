using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Activation : MonoBehaviour
{
    private const float stonePillarCD = 10f;  // Tempo de cooldown
    private const float stonePillarRange = 300f;  // Alcance
    private bool stonePillarAvailable = true;  // Verifica se a habilidade está disponível
    private LayerMask playerMask;  // Layer para detectar o jogador
    private SP_Invoke invokeStonePillar;  // Componente para invocar o pilar
    protected Collider2D[] entities;  // Array de entidades encontradas

    private void Start()
    {
        playerMask = LayerMask.GetMask("Player");  // Máscara para o jogador
        invokeStonePillar = GetComponent<SP_Invoke>();  // Obter o componente SP_Invoke
    }

    // Detecta se o jogador está dentro do alcance
    public void DetectPlayer()
    {
        if(!stonePillarAvailable) return;  // Se a habilidade não estiver disponível, sai do método

        // Detecta todas as entidades dentro da área de alance
        entities = Physics2D.OverlapCircleAll(transform.position, stonePillarRange, playerMask);
        if (entities.Length > 0)
        {
            // Ativa a skill ao detectar o jogador
            UseStonePillar();
        }
    }

    // Método para usar o StonePillar
    private void UseStonePillar()
    {
        // Inicia o efeito de invocar o pilar e o cooldown
        StartCoroutine(invokeStonePillar.Invoke_StonePillar());
        StartCoroutine(StonePillarResetCooldown());
        stonePillarAvailable = false;  // Marca a habilidade como indisponível
    }

    // Método que controla o cooldown
    private IEnumerator StonePillarResetCooldown()
    {
        yield return new WaitForSeconds(stonePillarCD);  // Espera o tempo de cooldown
        stonePillarAvailable = true;  // Torna a habilidade disponível novamente
    }

    // Método para desenhar o raio de detecção no editor (Gizmos)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;  // Define a cor do gizmo
        Gizmos.DrawWireSphere(transform.position, stonePillarRange);  // Desenha o raio de detecção
    }
}
