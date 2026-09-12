using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Animator animator;
    private bool isGrounded;
    private bool facingRight = true;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento Horizontal
        float move = Input.GetAxisRaw("Horizontal");
        rd.velocity = new Vector2(move * moveSpeed, rd.velocity.y);

        // Animación de correr/caminar
        animator.SetFloat("Speed", Mathf.Abs(move));

        // Cambio de dirección
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("IsJumping", true);
        }
    }

    // Detección de suelo
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false); // Ya aterrizó
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("IsJumping", true); // Empezó a caer o saltó
        }
    }

    // Voltear el sprite
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}