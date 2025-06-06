using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBars : MonoBehaviour
{
    [Header("Cor do Pisque")]
    [SerializeField] private Color blinkColor = Color.red;
    [Header("Componentes UI")]
    public Slider slider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private StatusComponent status;
    [SerializeField] private PlayerSound playerSound;

    private int blinkCount = 3;
    private float blinkDuration = 0.05f; // tempo entre os pisques
    private bool isHealing = false; // flag para verificar se está regenerando vida
    private Color originalColor;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    private void Update()
    {

    }
    public void SetHealth(float currentHealth)
    {
        slider.value = currentHealth;
    }

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void TakeDamage(float damage) // Método para receber dano
    {
        if (!isHealing)
        {
            isHealing = true;
            StartCoroutine(HealthRegen()); // Inicia a regeneração de vida se estiver abaixo do máximo
        }
        playerSound.PlayHurt();
        float posDamage = (damage * 100) / (status.currentDefense + 100);
        status.ModifyCurrentValue(AttributeType.currentHealth, -posDamage);
        SetHealth(status.currentHealth);
        StartCoroutine(BlinkColorEffect()); // Inicia o efeito de piscar

    }

    IEnumerator BlinkColorEffect()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            spriteRenderer.color = blinkColor;
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    IEnumerator HealthRegen()
    {
        while (status.currentHealth < status.maxHealth)
        {
            status.ModifyCurrentValue(AttributeType.currentHealth, status.currentHealthRegen);
            SetHealth(status.currentHealth);
            yield return new WaitForSeconds(1f); // Espera 1 segundo antes de regenerar novamente
        }
        isHealing = false; // Reseta a flag de regeneração
    }
}
