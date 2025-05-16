using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayerOnAttack : MonoBehaviour
{
    private Collider2D[] findPlayer;
    private Vector2 GizmosPosition;
    [SerializeField] private StatusComponent status;

    public void FindPlayer(Vector2 pointOfReference) // Método para encontrar o jogador
    {
        //PointOfReference é a posição de origem do raycast que vai detectar o player
        // Verifica se há inimigos dentro do diâmetro de 1 unidade
        GizmosPosition = pointOfReference;
        findPlayer = Physics2D.OverlapCircleAll(pointOfReference, 0.5f, LayerMask.GetMask("Player"));
        
        foreach (Collider2D enemy in findPlayer)
        {
            if (enemy.name == "Kenzen")
            {
                enemy.GetComponent<PlayerBars>().TakeDamage(status.currentAttackDamage); // Dano do ataque
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GizmosPosition, 0.5f);
    }
}
