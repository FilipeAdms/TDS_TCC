using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private float cameraSpeed = 2f; // Velocidade da câmera
    public float yOffset = 15f; // Offset na posição Y da câmera
    public Transform playerPosition; // Referência ao jogador


    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(playerPosition.position.x, playerPosition.position.y + yOffset, -10f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }
}
