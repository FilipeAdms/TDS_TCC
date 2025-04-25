using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// StatusComponent é um componente que gerencia os atributos de uma unidade
public class StatusComponent : MonoBehaviour
{
    [Header("Atributos da Unidade")]
    public AttributeSet attributeSet = new();

    [Header("Atributos")]
    public float Health = 100f;
    public float Aura = 100f;
    public float MoveSpeed = 125f;
    public float AttackDamage = 20f;
    public float Defense = 20f;
    public float AuraRegen = 1.25f;
    public float HealthRegen = 1.75f;


    private void Awake() // 
    {
        // Adiciona os atributos iniciais
        attributeSet.AddAttribute(AttributeType.Health, Health);
        attributeSet.AddAttribute(AttributeType.Aura, Aura);
        attributeSet.AddAttribute(AttributeType.MoveSpeed, MoveSpeed);
        attributeSet.AddAttribute(AttributeType.AttackDamage, AttackDamage);
        attributeSet.AddAttribute(AttributeType.Defense, Defense);
        attributeSet.AddAttribute(AttributeType.AuraRegen, AuraRegen);
        attributeSet.AddAttribute(AttributeType.HealthRegen, HealthRegen);
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
