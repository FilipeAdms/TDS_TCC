using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MahhorSkills : MonoBehaviour
{
    [SerializeField] private SP_Activation spActivation;
    [SerializeField] private Stalagmite stalagmite;

    private void Start()
    {
        spActivation = GetComponent<SP_Activation>();
        stalagmite = GetComponent<Stalagmite>();
    }

    public void ChooseSkill()
    {
        // Escolhe uma habilidade aleatória
        float skillIndex = Random.Range(1, 100);
        /* 
        Stalagmite
        Stone Pillar
        Corrupção Instável
        Terremoto
         */
        if (skillIndex <= 41) // 41% de chance de ativar Stalagmite
        {
            stalagmite.StartStalagmite();
        }
        else if (skillIndex > 41 && skillIndex <= 82) // 41% de chance de ativar Stone Pillar
        {
            spActivation.DetectPlayer();
        }
        else if (skillIndex > 82 && skillIndex <= 100) // 18% de chance de ativar Corrupção Instável
        {
            Debug.Log("Corrupção Instável");
        }
        else if (skillIndex > 100) // 1% de chance de ativar Terremoto
        {
            Debug.Log("Terremoto");
        }

    }
}
