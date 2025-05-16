using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagmit : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private GameObject stalagmitPrefab;
    [SerializeField] private MahhorSkillController skillController;
    public float stalagmitRange;
    public float stalagmitAmount;
    private Collider2D[] playerDetection;

    
    // O método RangeDetection serve para detectar se o jogador está dentro da área de alcance da habilidade
    public void RangeDetection()
    {
        playerDetection = Physics2D.OverlapCircleAll(transform.position, stalagmitRange, playerMask);

        foreach (Collider2D collider in playerDetection)
        {
            if (collider.CompareTag("Player"))
            {
                StartCoroutine(SpawnCooldown(collider));
                Debug.Log("Stalagmite Cooldown iniciado");
                skillController.StartCoroutine(skillController.StalagmitCooldown());
            }
        }
    }

    //Espaço de tempo para invocar um pilar depois do outro
    private IEnumerator SpawnCooldown(Collider2D collider)
    {
        for (int i = 0; i <= stalagmitAmount; i++)
        {
            Vector3 playerCurrentPosition = collider.transform.position;
            yield return new WaitForSeconds(0.2f);
            Instantiate(stalagmitPrefab, playerCurrentPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.25f);
        }
        skillController.canAct = true;
        skillController.ChooseSkill();
    }

}
