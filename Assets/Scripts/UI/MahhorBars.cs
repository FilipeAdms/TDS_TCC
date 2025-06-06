using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class MahhorBars : MonoBehaviour
{
    [Header("Cor do Pisque")]
    [SerializeField] private Color blinkColor = Color.red;
    [Header("Componentes UI")]
    public Slider slider;
    public TextMeshProUGUI healthText;
    [SerializeField] private StatusComponent status;
    [SerializeField] private MahhorSound mahhorSound;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private int blinkCount = 3;
    private float blinkDuration = 0.05f; // tempo entre os pisques

    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }
    
    public void SetHealth(float currentHealth)
    {
        slider.value = currentHealth; // Atualiza o valor do slider com a vida atual
        healthText.text = $"{(int)currentHealth} / {(int)slider.maxValue}";
    }

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth; // Define o valor máximo do slider
    }

    public void TakeDamage(float damage)
    {
        mahhorSound.PlayHurt();
        // posDamage -> dano pós mitigação
        float posDamage = (damage * 100) / (status.currentDefense + 100); // Cálculo da mitigação de dano
        status.ModifyCurrentValue(AttributeType.currentHealth, -posDamage); // Subtrai o dano da vida atual
        SetHealth(status.currentHealth); // Atualiza a barra de vida
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
}
