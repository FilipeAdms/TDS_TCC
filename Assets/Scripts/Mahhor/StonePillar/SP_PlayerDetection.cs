using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_PlayerDetection : MonoBehaviour
{
    private bool canDamage = true;
    private Vector3 knockBackDirection;
    private float knockBackForce = 100f;
    
    private SpriteRenderer teste;

    private void Start()
    {
        teste = GetComponent<SpriteRenderer>();
        StartCoroutine(DisableDamage());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canDamage && collision.gameObject.CompareTag("Player"))
        {

            Health_Kenzen healthKenzen = collision.gameObject.GetComponent<Health_Kenzen>();
            Rigidbody2D playerRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRigidBody != null && healthKenzen != null && canDamage)
            {
                StopAllCoroutines();
                canDamage = false;
                teste.color = Color.red;
                healthKenzen.TakeDamage(25);
                knockBackDirection = (collision.transform.position - transform.position).normalized;
                playerRigidBody.AddForce(knockBackDirection * knockBackForce, ForceMode2D.Impulse);
            }
        }
    }

    private IEnumerator DisableDamage()
    {
        yield return new WaitForSeconds(0.15f);
        canDamage = false;
        teste.color = Color.gray;
    }


}
