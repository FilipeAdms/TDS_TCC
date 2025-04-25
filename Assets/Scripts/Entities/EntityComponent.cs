using UnityEngine;

public class EntityComponent : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // Referência ao Rigidbody2D
    [SerializeField] private Transform transformCompo;
    [SerializeField] private Animator animator;
    [SerializeField] private StatusComponent statusComponent;

    // Propriedades públicas para acessar os componentes
    public Rigidbody2D Rigidbody => rb;
    public Transform Transform => transformCompo;
    public Animator Animator => animator;
    public StatusComponent StatusComponent => statusComponent;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (transformCompo == null)
        {
            transformCompo = GetComponent<Transform>();
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (statusComponent == null)
        {
            statusComponent = GetComponent<StatusComponent>();
        }
    }
}
