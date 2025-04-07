using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class Aura_Kenzen : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI textMeshPro;
    private Stats_Data stats_data;

    private float aura;
    private float maxAura;

    void Start()
    {
        stats_data = GetComponent<Stats_Data>();
        AtualizeStats();
        SetAura();
    }

    void Update()
    {
        Teste();
    }

    private void SetAura()
    {
        if (stats_data != null )
        {
            if(aura > maxAura)
            {
                aura = maxAura;
            }
            else if (aura < 0)
            {
                aura = 0;
            }
            slider.maxValue = maxAura;
            slider.value = aura;
            stats_data.characterData.aura = aura;
            textMeshPro.text = $"{aura} / {maxAura}";
        }
    }

    private void AtualizeStats()
    {
        if (stats_data != null)
        {
            aura = stats_data.characterData.aura;
            maxAura = stats_data.characterData.maxAura;
        }
    }

    private void TakeDamage(float incomingDamage)
    {
        aura -= incomingDamage;
        SetAura();
    }

    private void Teste()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            TakeDamage(50);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            aura += maxAura * 0.35f;
            SetAura();
        }
    }
}
