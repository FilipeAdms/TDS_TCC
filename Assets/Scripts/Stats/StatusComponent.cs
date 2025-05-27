using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

// StatusComponent é um componente que gerencia os atributos de uma unidade
public class StatusComponent : MonoBehaviour
{
    // Valores atuais
    [Header("Atributos atuais")]
    public float currentHealth = 100f;
    public float currentAura = 100f;
    public float currentMoveSpeed = 4f;
    public float currentAttackDamage = 20f;
    public float currentDefense = 20f;
    public float currentAuraRegen = 1.25f;
    public float currentHealthRegen = 1.75f;

    // Valores máximos
    [Header("Atributos Base")]
    public float maxHealth = 100f;
    public float maxAura = 100f;
    public float baseMoveSpeed = 4f;
    public float baseAttackDamage = 20f;
    public float baseDefense = 20f;
    public float baseHealthRegen = 1.25f;
    public float baseAuraRegen = 1.75f;

    // Valores iniciais (para usar como referência)
    [Header("Atributos Base")]
    public float inicialHealth = 100f;
    public float inicialAura = 100f;
    public float inicialMoveSpeed = 4f;
    public float inicialAttackDamage = 20f;
    public float inicialDefense = 20f;
    public float inicialHealthRegen = 1.25f;
    public float inicialAuraRegen = 1.75f;

    public void ModifyCurrentValue(AttributeType type, float value) // Modifica o valor de um atributo
    {
        switch (type)
        {
            case AttributeType.currentHealth:
                currentHealth = currentHealth + value;
                break;
            case AttributeType.currentAura:
                currentAura = currentAura + value;
                break;
            case AttributeType.currentMoveSpeed:
                currentMoveSpeed = currentMoveSpeed + value;
                break;
            case AttributeType.currentAttackDamage:
                currentAttackDamage = currentAttackDamage + value;
                break;
            case AttributeType.currentDefense:
                currentDefense = currentDefense + value;
                break;
            case AttributeType.currentAuraRegen:
                currentAuraRegen = currentAuraRegen + value;
                break;
            case AttributeType.currentHealthRegen:
                currentHealthRegen = currentHealthRegen + value;
                break;
            default:
                break;
        }
    }
    public void ModifyBaseValue(AttributeType type, float value)
    {
        switch (type)
        {
            case AttributeType.maxHealth:
                maxHealth = Mathf.Max(maxHealth + value, 0);
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
                break;
            case AttributeType.maxAura:
                maxAura = Mathf.Max(maxAura + value, 0);
                currentAura = Mathf.Clamp(currentAura, 0, maxAura);
                break;
            case AttributeType.baseMoveSpeed:
                baseMoveSpeed = Mathf.Max(baseMoveSpeed + value, 0);
                break;
            case AttributeType.baseAttackDamage:
                baseAttackDamage = Mathf.Max(baseAttackDamage + value, 0);
                break;
            case AttributeType.baseDefense:
                baseDefense = Mathf.Max(baseDefense + value, 0);
                break;
            case AttributeType.baseAuraRegen:
                baseAuraRegen = Mathf.Max(baseAuraRegen + value, 0);
                break;
            case AttributeType.baseHealthRegen:
                baseHealthRegen = Mathf.Max(baseHealthRegen + value, 0);
                break;
            default:
                break;
        }
    }

    public void ResetCurrentValue(AttributeType type)
    {
        switch (type)
        {
            case AttributeType.currentHealth:
                currentHealth = inicialHealth;
                break;
            case AttributeType.currentAura:
                currentAura = inicialAura;
                break;
            case AttributeType.currentMoveSpeed:
                currentMoveSpeed = inicialMoveSpeed;
                break;
            case AttributeType.currentAttackDamage:
                currentAttackDamage = inicialAttackDamage;
                break;
            case AttributeType.currentDefense:
                currentDefense = inicialDefense;
                break;
            case AttributeType.currentAuraRegen:
                currentAuraRegen = inicialHealthRegen;
                break;
            case AttributeType.currentHealthRegen:
                currentHealthRegen = inicialAuraRegen;
                break;
            default:
                break;
        }
    }

    public void ResetBaseValue(AttributeType type)
    {
        switch (type)
        {
            case AttributeType.currentHealth:
                maxHealth = inicialHealth;
                break;
            case AttributeType.currentAura:
                maxAura = inicialAura;
                break;
            case AttributeType.currentMoveSpeed:
               baseMoveSpeed = inicialMoveSpeed;
                break;
            case AttributeType.currentAttackDamage:
               baseAttackDamage = inicialAttackDamage;
                break;
            case AttributeType.currentDefense:
               baseDefense = inicialDefense;
                break;
            case AttributeType.currentAuraRegen:
               baseAuraRegen = inicialHealthRegen;
                break;
            case AttributeType.currentHealthRegen:
               baseHealthRegen = inicialAuraRegen;
                break;
            default:
                break;
        }
    }

}
