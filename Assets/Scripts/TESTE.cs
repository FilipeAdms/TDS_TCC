using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TESTE : MonoBehaviour
{
    public Stats_Data stats_Data;
    [SerializeField] private TextMeshProUGUI statsTextMash;

    private void Update()
    {
        SetAtt();
    }

    void SetAtt()
    {
        statsTextMash.text = $"Defense: {stats_Data.characterData.defense}\n" +
            $"Mov. Speed: {stats_Data.characterData.movSpeed}\n" +
            $"Health Regen: {stats_Data.characterData.healthRegen}\n" +
            $"";
    }
  
}
