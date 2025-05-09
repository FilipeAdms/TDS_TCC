using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePillar : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private GameObject stonePillarPrefab;
    [SerializeField] private MahhorSkillController SkillController;
    public float stonePillarRange;
    public float stonePillarAmount;
    private Collider2D[] playerDetection;


    // O método RangeDetection serve para detectar se o jogador está dentro da área de alcance da habilidade
    public void RangeDetection()
    {
        Debug.Log("Tentando Detectar o jogador");
        playerDetection = Physics2D.OverlapCircleAll(transform.position, stonePillarRange, playerMask);

        foreach (Collider2D collider in playerDetection)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log("Jogador Detectado, iniciando Corrotina");
                Vector3 playerCurrentPosition = collider.transform.position;
                StartCoroutine(SpawnCooldown(collider, playerCurrentPosition));
                StartCoroutine(Cooldown());
            }
        }
    }

    private IEnumerator SpawnCooldown(Collider2D collider, Vector3 PlayerPosition)
    {
        Vector3 direction = (PlayerPosition - this.transform.position).normalized;

        for (int i = 0; i <= stonePillarAmount; i++)
        {
            Vector3 spawnPosition = this.transform.position + direction * 0.5f * i;
            Instantiate(stonePillarPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.05f);

        }
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(5f);
        SkillController.ChooseSkill();
    }
}
