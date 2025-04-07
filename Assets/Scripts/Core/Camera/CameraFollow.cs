using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private float cameraSpeed = 2f; // Velocidade da c�mera
    public float yOffset = 15f; // Offset na posi��o Y da c�mera
    public Transform playerPosition; // Refer�ncia ao jogador


    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(playerPosition.position.x, playerPosition.position.y + yOffset, -10f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }
}
