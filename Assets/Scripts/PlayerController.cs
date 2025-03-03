using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public TMP_Text jumpsLeft;
    private PlayerState state;
    public int jumps = 5;
    public GameObject arrow;
    public bool isOnGround = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = new PlayerStateNormal(this);
        arrow = GameObject.Find("AimArrow");
        arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateJumps();

        if (Input.GetKey("right")) {
            state.HandleRight();
        }
        if (Input.GetKey("left")) {
            state.HandleLeft();
        }
        if (Input.GetKeyDown("space")) {
            state.HandleJump();
        }

        state.AdvanceState();
    }

    public void SetState(PlayerState s) {
        state = s;
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

    public void ShowAimArrow() {
        arrow.SetActive(true);
    }

    public void HideAimArrow() {
        arrow.SetActive(false);
        arrow.transform.rotation = Quaternion.identity;
    }

    public void Move(float force) {
        rb.linearVelocity = new Vector2(force + rb.linearVelocity.x, rb.linearVelocity.y);
        if (!(state is PlayerStateFreefall)) {
            CapVelocity();
        }
    }

    public void Jump(float force) {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, force);
    }

    void CapVelocity() {
        if (rb.linearVelocity.x > 5) {
            rb.linearVelocity = new Vector2(5, rb.linearVelocity.y);
        }
        if (rb.linearVelocity.x < -5) {
            rb.linearVelocity = new Vector2(-5, rb.linearVelocity.y);
        }
    }

    //transfer updatejumps out of player to fulfill single responsibility later
    public void UpdateJumps() {
        jumpsLeft.text = ""+jumps;
    }
}
