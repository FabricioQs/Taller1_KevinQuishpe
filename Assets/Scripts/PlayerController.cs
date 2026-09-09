using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private bool isGrounded;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        // Movimiento Horizontal
        float move = Input.GetAxis("Horizontal");
        rd.velocity = new Vector2(move * moveSpeed, rd.velocity.y);

        // Salto con Input.GetButtonDown("Jump") 
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Detectar cuando toca la plataforma
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Detectar cuando sale de la plataforma 
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}