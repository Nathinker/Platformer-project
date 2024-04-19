using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private ContactFilter2D groundFilter;
    private Vector2 inputDir = Vector2.zero;
    private CapsuleCollider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private Transform playerObject;
    public int gravityMode = 0; 
    private float colliderOffsetY = 0;
    private bool grounded = false;
    private bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerObject = GetComponent<Transform>();
        colliderOffsetY = coll.offset.y;
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = inputDir.x != 0;
        // Checks the approperiate animaton for the actions of the player character
        anim.SetBool("Run", isRunning);
        anim.SetBool("Grounded", grounded);
        if (gravityMode == 0) // Checks if gravity is downward
        {
            if (coll.offset.y == -colliderOffsetY)
            {
                coll.offset = new Vector2(coll.offset.x, -coll.offset.y);
            }
            groundFilter.minNormalAngle = 80;
            groundFilter.maxNormalAngle = 100;
            anim.SetFloat("DirY", rb.velocity.y);
            sprite.flipY = false;
            //coll.transform.rotation.Set(0, 0, 0, 0);
        }
        else if (gravityMode == 1) // Checks if gravity is upward
        {
            if (coll.offset.y == colliderOffsetY)
            {
                coll.offset = new Vector2(coll.offset.x, -coll.offset.y);
            }
            groundFilter.minNormalAngle = -80;
            groundFilter.maxNormalAngle = -100;
            anim.SetFloat("DirY", -(rb.velocity.y));
            sprite.flipY = true;
            //coll.transform.rotation.Set(180, 0, 0, 0);
        }
        grounded = coll.IsTouching(groundFilter);
        MoveObject();
        Jump();
        GravitySwitch();
    }

    // Moves the player
    private void MoveObject()
    {
        Vector2 newVelocity = rb.velocity;
        newVelocity.x = inputDir.x * speed;
        rb.velocity = newVelocity;
    }

    // Checks if the player is grounded and jumps if they are
    private void Jump()
    {
        if (jump && grounded)
        {
            jump = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            if (gravityMode == 0) // Checks if gravity is downward, and normalizes the jump force
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (gravityMode == 1) // Checks if gravity is upward, and flips the jump force
            {
                rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    // Sets the move direction when the left or right keys are pressed, and flips the sprite if necessary
    public void setMoveDir(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();
        bool flipX = inputDir.x < 0;

        if (sprite.flipX != flipX)
        {
            sprite.flipX = flipX;
        }
    }

    // Actives jump when the jump key is pressed
    public void ActivateJump(InputAction.CallbackContext context)
    {
        jump = context.started;
    }

    // Switches the gravity when the Q key is pressed
    public void GravitySwitch()
    {
        if (Input.GetKeyDown(KeyCode.Q) && grounded)
        {
            switch (gravityMode)
            {
                case 0: // switches to downward gravity
                    gravityMode = 1;
                    rb.gravityScale = -1.25f;
                    break;
                case 1: // switches to upward gravity
                    gravityMode = 0;
                    rb.gravityScale = 1.25f;
                    break;
                default:
                    break;
            }
        }
    }
}
