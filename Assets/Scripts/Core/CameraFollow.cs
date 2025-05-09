using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerPos;

    void LateUpdate()
    {
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y, -10f);
    }

}
