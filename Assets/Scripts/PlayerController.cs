using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState state;
    public Vector3 getvelo;
    public int jumps = 5;
    public bool isOnGround = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = new PlayerStateNormal(this);
    }

    // Update is called once per frame
    void Update()
    {
        getvelo = rb.linearVelocity;
        if (Input.GetKey("right")) {
            state.HandleRight();
        }
        if (Input.GetKey("left")) {
            state.HandleLeft();
        }
        if (Input.GetKeyDown("space") && isOnGround && jumps > 0) {
            jumps--;
            state.HandleJump();
            isOnGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }

    public void Move(int force) {
        rb.velocity = new Vector2(force * Time.deltaTime, rb.velocity.y);
    }

    public void Jump(int force) {
        rb.linearVelocity = new Vector2(rb.velocity.x, force * Time.deltaTime);
    }
}
