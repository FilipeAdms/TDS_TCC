using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagmite : MonoBehaviour
{
    private Transform kenzenTransform;
    private Vector3 kenzenCurrentPosition;
    [SerializeField] private GameObject stalagmitePrefab;

    private void Start()
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
            kenzenCurrentPosition = kenzenTransform.position;
            Instantiate(stalagmitePrefab, kenzenCurrentPosition, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
