using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// StatusComponent é um componente que gerencia os atributos de uma unidade
public class StatusComponent : MonoBehaviour
{
    [Header("Atributos da Unidade")]
    public AttributeSet attributeSet = new();

    [Header("Atributos atuais")]
    public float currentHealth = 100f;
    public float currentAura = 100f;
    public float currentMoveSpeed = 2.5f;
    public float currentAttackDamage = 20f;
    public float currentDefense = 20f;
    public float currentAuraRegen = 1.25f;
    public float currentHealthRegen = 1.75f;
    [Header("Atributos Base")]
    public float maxHealth = 100f;
    public float maxAura = 100f;
    public float baseMoveSpeed = 2.5f;
    public float baseAttackDamage = 20f;
    public float baseDefense = 20f;
    public float baseHealthRegen = 1.25f;
    public float baseAuraRegen = 1.75f;


    private void Awake() // 
    {
        // Adiciona os atributos iniciais
        attributeSet.AddAttribute(AttributeType.currentHealth, currentHealth);
        attributeSet.AddAttribute(AttributeType.maxHealth, maxHealth);
        attributeSet.AddAttribute(AttributeType.currentAura, currentAura);
        attributeSet.AddAttribute(AttributeType.maxAura, maxAura);
        attributeSet.AddAttribute(AttributeType.currentMoveSpeed, currentMoveSpeed);
        attributeSet.AddAttribute(AttributeType.baseMoveSpeed, baseMoveSpeed);
        attributeSet.AddAttribute(AttributeType.currentAttackDamage, currentAttackDamage);
        attributeSet.AddAttribute(AttributeType.baseAttackDamage, baseAttackDamage);
        attributeSet.AddAttribute(AttributeType.currentDefense, currentDefense);
        attributeSet.AddAttribute(AttributeType.baseDefense, baseDefense);
        attributeSet.AddAttribute(AttributeType.currentHealthRegen, currentHealthRegen);
        attributeSet.AddAttribute(AttributeType.baseHealthRegen, baseHealthRegen);
        attributeSet.AddAttribute(AttributeType.currentAuraRegen, currentAuraRegen);
        attributeSet.AddAttribute(AttributeType.baseAuraRegen, baseAuraRegen);
    }

    public float GetCurrent(AttributeType type) // Pega o valor atual de um atributo
    {
        return attributeSet.GetCurrentValue(type);
    }

    public void Modify(AttributeType type, float value) // Modifica o valor de um atributo
    {
        attributeSet.ModifyAttribute(type, value);
    }

}
