using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


// A classe é "Serializable" para que a unity entenda que os dados podems ser convertidos para Json (ou algo assim)
[Serializable] public class CharacterData
{
    public string name;
    public int level;
    public float health;
    public float maxHealth;
    public float aura;
    public float maxAura;
    public float movSpeed;
    public float defense;    
    public float healthRegen;
    public float auraRegen;
    public Vector3 position;
}
    [Serializable] public class Charactercondition
{

}

/* Valores padrões
    Stats = new CharacterData
    {
        name = "NOME",
        level = 1,
        health = 100f,
        maxHealth = 100f,
        aura = 100f,
        maxAura = 100f,
        movSpeed = 125f,
        defense = 25f,
        healthRegen = 0.25f,
        auraRegen = 1.25f
    };
*/

