using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable] // isso permite que a classe seja serializada e exibida no Inspector do Unity
public class AttributeSet // Classe que representa um conjunto de atributos de uma unidade
{
    // lista de atributos
    public List<Attribute> attributes = new List<Attribute>();

    //Adiciona um novo atributo
    public void AddAttribute(AttributeType type, float baseValue)
    {
        if (!HasAttribute(type))
        {
            attributes.Add(new Attribute(type, baseValue));
        }
    }

    //Retorna um atributo em específico
    public Attribute GetAttribute(AttributeType type)
    {
        return attributes.Find(attr => attr.type == type);
    }

    //Verifica se o atributo existe
    public bool HasAttribute(AttributeType type)
    {
        return attributes.Exists(attr => attr.type == type);
    }

    // Pegar o valor atual de um atributo
    public float GetCurrentValue(AttributeType type)
    {
        var attr = GetAttribute(type);
        return attr != null ? attr.currentValue : 0f;
    }

    //Modificar um atributo
    public void ModifyAttribute(AttributeType type, float amount)
    {
        var attr = GetAttribute(type);
        if (attr != null)
        {
            attr.SetCurrentValue(amount);
        }
    }
}
