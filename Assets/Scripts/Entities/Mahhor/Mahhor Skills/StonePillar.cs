using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePillar : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private GameObject stonePillarPrefab;
    [SerializeField] private MahhorSkillController skillController;
    public float stonePillarRange;
    public float stonePillarAmount;
    private Vector3 playerPosOffset;
    private Collider2D[] playerDetection;


    // O método RangeDetection serve para detectar se o jogador está dentro da área de alcance da habilidade
    public void RangeDetection()
    {
        Debug.Log("Tentando Detectar o jogador");
        playerDetection = Physics2D.OverlapCircleAll(transform.position, stonePillarRange, playerMask);
        if (playerDetection.Length == 0)
        {
            Debug.Log("Jogador não detectado");
            skillController.canAct = true;
            skillController.isStalagmitActive = false;
            skillController.ChooseSkill();
            return;
        }
        foreach (Collider2D collider in playerDetection)
        {
            if (collider.CompareTag("Player"))
            {
                playerPosOffset = new Vector3(0, -0.2f, 0);
                Debug.Log("Jogador Detectado, iniciando Corrotina");
                Vector3 playerCurrentPosition = collider.transform.position + playerPosOffset;
                StartCoroutine(SpawnCooldown(collider, playerCurrentPosition));

                Debug.Log("Stalagmite Cooldown iniciado");
                skillController.StartCoroutine(skillController.StonePillarCooldown());
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
        skillController.canAct = true;
        skillController.ChooseSkill();
    }
}
