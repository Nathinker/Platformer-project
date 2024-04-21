using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private ContactFilter2D groundFilter;

    private Vector2 inputDir = Vector2.zero;
    private CapsuleCollider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private Transform playerTransform;
    public int playerFlipState = 0;
    public int gravityState = 0; 
    private bool isGrounded = false;
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = inputDir.x != 0;
        
        // Checks the approperiate animaton for the actions of the player character
        anim.SetBool("Run", isRunning);
        anim.SetBool("Grounded", isGrounded);
        float dirY = rb.velocity.y;
        groundFilter.minNormalAngle = -80f * (gravityState == 0 ? -1 : gravityState == 1 ? 1 : -1);
        groundFilter.maxNormalAngle = -100f * (gravityState == 0 ? -1 : gravityState == 1 ? 1 : -1);
        anim.SetFloat("DirY", dirY * (gravityState == 0 ? 1 : gravityState == 1 ? -1 : 1));
        playerTransform.rotation = Quaternion.Euler(0, 0, 180f * (gravityState == 0 ? 0 : gravityState == 1 ? 1 : 0));
        isGrounded = coll.IsTouching(groundFilter);
        sprite.flipX = (playerFlipState == 1) != (gravityState == 1);
        MovePlayer();
        GravitySwitch();
    }

    // Moves the player
    private void MovePlayer()
    {
        Vector2 newVelocity = rb.velocity;
        newVelocity.x = inputDir.x * moveSpeed;
        rb.velocity = newVelocity;
    }

    // Checks if the player is grounded and jumps if they are
    private void Jump()
    {
        if (isJumping && isGrounded)
        {
            isJumping = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            if (gravityState == 0) // Checks if gravity is downward, and normalizes the isJumping force
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (gravityState == 1) // Checks if gravity is upward, and flips the isJumping force
            {
                rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    // Sets the move direction when the left or right keys are pressed, and flips the sprite if necessary
    public void SetMoveDirection(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();
        bool flipX = inputDir.x < 0;

        if (inputDir.x == -1) // Checks if the player is facing left
        {
            playerFlipState = 1;
        }
        else if (inputDir.x == 1) // Checks if the player is facing right
        {
            playerFlipState = 0;
        }

        sprite.flipX = flipX ^ (gravityState == 1);
    }

    // Actives isJumping when the jump key is pressed
    public void ActivateJump(InputAction.CallbackContext context)
    {
        isJumping = context.started;
    }

    // Switches the gravity when the Q key is pressed
    public void GravitySwitch()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isGrounded)
        {
            gravityState = 1 - gravityState;
            rb.gravityScale *= -1;
            sprite.flipX = gravityState == 1;
        }
    }

    public void RespawnPositioning()
    {
        gravityState = 0;
        rb.gravityScale = 1.25f;
        sprite.flipX = gravityState == 1;
    }
}
