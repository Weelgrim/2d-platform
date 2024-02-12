using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement vars")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private bool isGrounded = false;

    [Header("Setings")]
    [SerializeField] private Transform groundColliderTransform;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float jumpOffset, airOffset;
    [SerializeField] private LayerMask groundMask;

    [Header("")]
    public Transform sword;
    public Health health;
    public GameObject deathSceen;
    public Counter counter;

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Quaternion attR;
    private Quaternion attL;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        attR = Quaternion.Euler(0, 0, 180);
        attL = Quaternion.Euler(0, 0, 0);
        counter = GameObject.Find("Popap").GetComponent<Counter>();
    }

    private void FixedUpdate()
    {
        Vector3 overlapCirclePosition = groundColliderTransform.position;
        isGrounded = Physics2D.OverlapCircle(overlapCirclePosition, jumpOffset, groundMask);
        if (isGrounded)
            animator.SetBool("InAir", false);
        else InAir();
    }
    public void Move(float direction, bool isJumpButtonPressed)
    {
        if (health.isAlive == true)
        {
            animator.SetBool("Run", true);

            if (direction > 0)
            {
                sprite.flipX = false;
                sword.rotation = attL;
            }
            else if (direction < 0)
            {
                sprite.flipX = true;
                sword.rotation = attR;
            }

            if (isJumpButtonPressed)
            {
                animator.SetTrigger("Jump");
                Jump();
            }


            if (Mathf.Abs(direction) > 0.01f)
                HorizontalMovement(direction);
            else animator.SetBool("Run", false);
        }      
    }

    private void Jump()
    {
        if (isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);           
    }

    private void HorizontalMovement(float direction)
    {
        rb.velocity = new Vector2(curve.Evaluate(direction), rb.velocity.y);
    }

    private void Update()
    {
        if(health.isAlive == false)
        {
            deathSceen.SetActive(true);
        }
    }

    private void InAir()
    {
        animator.SetBool("InAir", true);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            counter.GetPointsforCoin();
        }
    }
}
