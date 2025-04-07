using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class SP_Invoke : MahhorSkills
{

    [SerializeField] private GameObject prefabStonePillar;
    [SerializeField] private SP_Animation spAnimation;
    private Vector3 playerPosition;
    private Vector3 playerDirection;
    private readonly float pillarRadius = 25f;

    private readonly List<GameObject> spawnedPillars = new(); // Lista pra guardar os pilares criados

    public IEnumerator Invoke_StonePillar()
    {
        playerPosition = transform.position;
        if (entities.Length > 0)
        {
            playerPosition = entities[0].transform.position;
        }
        playerDirection = (playerPosition - transform.position).normalized;

        // Quantidade de pilares que ser�o spawnados
        int numberOfPillars = 15;

        // Spawna os pilares em linha reta na dire��o do mouse
        for (int i = 1; i <= numberOfPillars; i++)
        {
            Vector3 spawnPosition = transform.position + (i * pillarRadius * playerDirection);
            GameObject pillar = Instantiate(prefabStonePillar, spawnPosition, Quaternion.identity);
            spawnedPillars.Add(pillar);
            /* Em resumo, um instantiate cria um objeto, ou seja, ele tamb�m retorna este objeto,
             sendo assim, posso guardar este objeto numa vari�vel do tipo GameObject, desta maneira
            posso fazer refer�ncias a este objeto*/
            yield return new WaitForSeconds(0.05f);
        }

    }

    public void StartDestroyPillar()
    {
        StartCoroutine(DestroyPillar());
    }
    private IEnumerator DestroyPillar()
    {
        List<GameObject> pillarsToDestroy = new List<GameObject>(spawnedPillars); // Cria uma c�pia para evitar um erro esquisito

        foreach (GameObject pillar in pillarsToDestroy)
        {
            yield return new WaitForSeconds(0.05f);
            Destroy(pillar);
        }
        spawnedPillars.Clear(); // Limpa a lista ap�s a destrui��o de todos os pilares
    }

}
