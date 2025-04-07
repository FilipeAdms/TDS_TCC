using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTest : MonoBehaviour
{

    private Stats_Data stats_Data;

    private float speedBuff = 2f;
    private float inicialSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        stats_Data = collision.GetComponent<Stats_Data>();
        Debug.Log($"Buff chamado, Speed atual: {stats_Data.characterData.movSpeed}");
        inicialSpeed = stats_Data.characterData.movSpeed;

        if (collision.CompareTag("Player") && stats_Data != null)
        {
            stats_Data.characterData.movSpeed *= speedBuff;
            Debug.Log($"Novo Speed: {stats_Data.characterData.movSpeed}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        stats_Data.characterData.movSpeed = inicialSpeed;
        Debug.Log($"Buff removido: {stats_Data.characterData.movSpeed}");
    }
}
