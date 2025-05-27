using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBars : MonoBehaviour
{
    public Slider slider;
    [SerializeField] private StatusComponent status;

    public void SetHealth(float currentHealth)
    {
        slider.value = currentHealth;
    }

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        float posDamage = (damage * 100) / (status.currentDefense + 100);
        status.ModifyCurrentValue(AttributeType.currentHealth, -posDamage);
        SetHealth(status.currentHealth);
    }
}
