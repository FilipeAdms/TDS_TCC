using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthText;
    [SerializeField] private StatusComponent status;

    public void SetHealth(float currentHealth)
    {
        slider.value = currentHealth;
        healthText.text = $"{currentHealth} / {slider.maxValue}";
    }

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        float posDamage = (damage * 100) / (status.GetCurrent(AttributeType.currentDefense) + 100);
        status.Modify(AttributeType.currentHealth, -posDamage);
        SetHealth(status.GetCurrent(AttributeType.currentHealth));
    }
}
