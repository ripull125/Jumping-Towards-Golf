using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public TMP_Text jumpsLeft;
    private PlayerState state;
    private Checkpoint lastPoint;
    public int jumps = 5;
    public GameObject arrow;
    public bool isOnGround = false;
    private SpriteRenderer sprite;

    public TMP_Text totalsText;
    public int totalJumps = 0;
    public int levelsCompleted = -1;
    private Animator anim;

    public 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = new PlayerStateNormal(this);
        arrow = GameObject.Find("AimArrow");
        arrow.SetActive(false);
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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
        if (Input.GetKeyDown("r")) {
            state.Respawn();
        }

        state.AdvanceState();
        UpdateAnimation();
    }

    public void SetState(PlayerState s) {
        state = s;
    }

    public void UpdateAnimation() {
        // First, reset all booleans to false
        anim.SetBool("IsNormal", false);
        anim.SetBool("IsJump", false);
        anim.SetBool("IsFreefall", false);
        anim.SetBool("IsDrive", false);
        anim.SetBool("IsLaunch", false);

        // Then, set the correct one based on the current state
        if (state is PlayerStateNormal) {
            if (Input.GetKey("right")) {
                sprite.flipX = false;
                anim.SetBool("IsWalking", true);
                anim.Play("PlayerStateWalking");
            } else if (Input.GetKey("left")){
                sprite.flipX = true;
                anim.SetBool("IsWalking", true);
                anim.Play("PlayerStateWalking");
            } else {
                anim.SetBool("IsNormal", true);
                anim.SetBool("IsWalking", false);
            }
        }
        else if (state is PlayerStateJumping) {
            anim.SetBool("IsJump", true);
            anim.Play("PlayerStateJumping");
        }
        else if (state is PlayerStateFreefall) {
            anim.SetBool("IsFreefall", true);
        }
        else if (state is PlayerStateDrive) {
            anim.SetBool("IsDrive", true);
        }
        else if (state is PlayerStateLaunch) {
            anim.SetBool("IsLaunch", true);
            anim.Play("PlayerStateLaunch");
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

    public void ShowAimArrow() {
        arrow.SetActive(true);
    }

    public void HideAimArrow() {
        arrow.SetActive(false);
        arrow.transform.rotation = Quaternion.identity;
    }

    public void Move(float force) {
        rb.linearVelocity = new Vector2(force + rb.linearVelocity.x, rb.linearVelocity.y);
        if (!(state is PlayerStateLaunch || state is PlayerStateFreefall || state is PlayerStateDrive)) {
            CapVelocity();
        }
    }

    public void Jump(float force) {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, force);
    }

    public void CapVelocity() {
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
        totalsText.text = "Jumps Used: "+ totalJumps + "\nLevels Completed: " + levelsCompleted;
    }

    public void ReachCheckpoint(Checkpoint c) {
        lastPoint = c;
        jumps = lastPoint.jumpsToComplete;
        GameObject cam = GameObject.Find("Main Camera");
        cam.transform.position = c.newCameraPos;
        if(c.interactedWith == false)
        {
            levelsCompleted++;
        }
    }

    public void Respawn() {
        transform.position = lastPoint.respawnPoint;
        jumps = lastPoint.jumpsToComplete;
    }
}
