using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenTeste : MonoBehaviour
{
    public Health_Kenzen health_Kenzen;
    private Stats_Data stats_Data;
    private float initialRegen;
    private readonly float regenBuff = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Isso faz com que o "playerController" receba os componentes do obeto que colidou com ele
        stats_Data= collision.GetComponent<Stats_Data>();
        initialRegen = stats_Data.characterData.healthRegen; // Salva o valor da regeneração inicial

        //Verifica se o colisor colidiu com o player
        if (collision.CompareTag("Player") && stats_Data!= null)
        {
            stats_Data.characterData.healthRegen *= regenBuff;
            health_Kenzen.AtulizeStats();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        stats_Data.characterData.healthRegen = initialRegen; // Retorna ao valor original
        health_Kenzen.AtulizeStats();
    }

}
