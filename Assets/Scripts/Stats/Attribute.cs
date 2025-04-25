using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute // Classe que representa um atributo de um personagem ou entidade
{

    public AttributeType type; // O tipo de atributo
    public float baseValue; // O valor base do atributo
    public float currentValue; // O valor atual do atributo

    public Attribute (AttributeType type, float baseValue) // Construtor que inicializa o tipo e o valor base do atributo
    {
        this.type = type;
        this.baseValue = baseValue;
        this.currentValue = baseValue;
    }

    public void SetCurrentValue(float amount)
    {
        // Amount (quantidade) é o valor a ser adicionado ou subtraído do valor atual
        currentValue += amount;
        
        // Garante que o valor atual não fique abaixo de 0 ou acima do valor base
        currentValue = Mathf.Clamp(currentValue, 0, baseValue);
    }

    public void SetBaseValue(float amount)
    {
        // Amount (quantidade) é o valor a ser adicionado ou subtraído do valor base
        baseValue += amount;
        // Garante que o valor base não fique abaixo de 0
        baseValue = Mathf.Max(baseValue, 0);
    }

    public void Reset()
    {
        currentValue = baseValue;
    }

}
