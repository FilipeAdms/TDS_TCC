using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //instancia global do PlayerController que s� pode ser modificado aqui
    public static PlayerController Instance { get; private set; }

    //Propriedade para facilitar o acesso do transform do PlayerController para skills do Mahhor
    public Transform KenzenTransform => transform;

    /*Garante que s� haja um �nico Player "Kenzen" na cena e destr�i os outros e
    garante que este objeto fique sempre dispon�vel para qualquer Script */
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
