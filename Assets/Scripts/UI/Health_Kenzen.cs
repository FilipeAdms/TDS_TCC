using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class Health_Kenzen : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI textMeshPro;
    public Stats_Data stats_Data;
    public float currentHealth;
    public float maxHealth;
    public float defense;
    public float healthRegen;

    private void Start()
    {
        AtulizeStats();
        SetMaxHealth();
    }

    private void Update()
    {
        PassiveHealing();
        Teste();
    }

    //Função para definir a vida atual
    public void SetHealth()
    {

        if (currentHealth > maxHealth) // Garante que a vida atual nunca será maior que a vida máxima
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth < 0) // Garante que a vida atual nunca seja negativa
        {
            currentHealth = 0;
        }
        stats_Data.characterData.health = currentHealth;
        slider.value = currentHealth; // Define a vida atual na barrade vida
        textMeshPro.text = $"{Mathf.Floor(currentHealth)} / {maxHealth}"; // Define a vida atual no texto ao lado da barra de vida
    }

    //Define a vida Máxima do personagem ao carregarr o jogo
    public void SetMaxHealth()
    {
        slider.maxValue = maxHealth; //Define a vida máxima
        slider.value = currentHealth; // Define a vida máxima na barrade vida
        textMeshPro.text = $"{Mathf.Floor(currentHealth)} / {maxHealth}"; // Escreve a vida atual / máxima no jogo
    }

    // Define o dano recebido
    public void TakeDamage(float incomingDamage)
    {
        currentHealth -= ((100 * incomingDamage) / (100 + defense)); // fórmula de dano
        SetHealth(); // Atualiza o novo Hp
    }

    // Regeneração Passiva
    private void PassiveHealing()
    {

        //Garantindo que só ative quando o jogador tomou algum dano
        if (currentHealth < maxHealth)
        {

            currentHealth += healthRegen * Time.deltaTime;
            SetHealth();

        }
    }

    private void Teste()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(50);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentHealth += maxHealth*0.35f;
            SetHealth();
        }
    }

    public void AtulizeStats()
    {
        currentHealth = stats_Data.characterData.health; // Atualizando o valor da vida atual numa variável mais simples para usar
        maxHealth = stats_Data.characterData.maxHealth; // Atualizando o valor da vida máxima numa variável mais simples para usar
        defense = stats_Data.characterData.defense; // Atualizando o valor da defesa numa variável mais simples para usar
        healthRegen = stats_Data.characterData.healthRegen; // Atualizando o valor da HealthRegen numa variável mais simples para usar

        Debug.Log($"AtulizeStats chamado! Novo healthRegen: {healthRegen}");
    }
}
