using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats_Data : MonoBehaviour
{
    public CharacterData characterData;

    private void Awake()
    {
        characterData = new CharacterData
        {
            name = gameObject.name,
            level = 1,
            health = 100f,
            maxHealth = 100f,
            aura = 100f,
            maxAura = 100f,
            movSpeed = 125f,
            defense = 25f,
            healthRegen = 0.25f,
            auraRegen = 1.25f,
            position = transform.position,
        };
    }
}

