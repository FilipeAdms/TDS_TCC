using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagmite : MonoBehaviour
{
    private Transform kenzenTransform;
    private Vector3 kenzenCurrentPosition;
    private const float posYOffset = -25f; // Offset para evitar que o stalagmite apareça em cima do Kenzen
    [SerializeField] private MahhorController mahhorController;
    [SerializeField] private GameObject stalagmitePrefab;

    private void Start()
    {
        mahhorController = GetComponent<MahhorController>();
    }
    public void StartStalagmite()
    {
        kenzenTransform = PlayerController.Instance != null ? PlayerController.Instance.KenzenTransform : null;

        if (kenzenTransform != null)
        {
            StartCoroutine(StalagmiteActivation());
        }
    }
    private IEnumerator StalagmiteActivation()
    {
        for (int i = 0; i < 5; i++)
        {
            kenzenCurrentPosition = new Vector3(kenzenTransform.position.x, kenzenTransform.position.y + posYOffset, 0f);
            Instantiate(stalagmitePrefab, kenzenCurrentPosition, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
        mahhorController.canAct = true; // Permite que o jogador ative outras habilidades
    }
}
