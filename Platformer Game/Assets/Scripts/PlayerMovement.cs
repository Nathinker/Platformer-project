using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

#region PlayerMovement Class
public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private TextMeshProUGUI dirYText;
    [SerializeField] private ContactFilter2D groundFilter;

    private Vector2 inputDir = Vector2.zero;
    private CapsuleCollider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private Transform playerTransform;
    public int playerFlipState = 0;
    public int gravityState = 0; 
    private bool IsGrounded => coll.IsTouching(groundFilter);
    private float dirY = 0;
    #endregion

    #region Unity Functions
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
        anim.SetBool("Grounded", IsGrounded);
        GrvaityParameters();
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Q) && playerTransform.localScale.x >= 1f && playerTransform.localScale.x <= 2f)
        {
            playerTransform.localScale /= 2f;
            moveSpeed *= 1.25f;
        }
        if (Input.GetKeyDown(KeyCode.E) && playerTransform.localScale.x <= 1f && playerTransform.localScale.x >= 0.5f)
        {
            playerTransform.localScale *= 2f;
            moveSpeed *= 0.8f;
        }
    }
#endregion

    #region Private Functions
    // Function to move the player
    private void MovePlayer()
    {
        Vector2 playerVelocity = new(inputDir.x * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
    }

    // Sets the move direction when the left or right keys are pressed, and flips the sprite if necessary
    public void SetMoveDirection(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();
        bool flipX = inputDir.x < 0;

        // Checks if the player is facing left or right and flips the sprite accordingly
        playerFlipState = (inputDir.x == -1) ? 1 : (inputDir.x == 1) ? 0 : playerFlipState;

        sprite.flipX = flipX ^ (gravityState == 1);
    }

    // Switches the gravity when the Q key is pressed
    public void GravitySwitch()
    {
        if (IsGrounded)
        {
            gravityState = 1 - gravityState;
            rb.gravityScale *= -1;
            sprite.flipX = gravityState == 1;
        }
    }

    public void GrvaityParameters()
    {
        dirY = rb.velocity.y;

        // Checks if the player has upwards or downnwards gravity and sets the approperiate parameters accordingly
        groundFilter.minNormalAngle = -80f * (gravityState == 0 ? -1 : gravityState == 1 ? 1 : -1);
        groundFilter.maxNormalAngle = -100f * (gravityState == 0 ? -1 : gravityState == 1 ? 1 : -1);
        anim.SetFloat("DirY", dirY * (gravityState == 0 ? 1 : gravityState == 1 ? -1 : 1));
        playerTransform.rotation = Quaternion.Euler(0, 0, 180f * (gravityState == 0 ? 0 : gravityState == 1 ? 1 : 0));
        sprite.flipX = (playerFlipState == 1) != (gravityState == 1);
        dirYText.text = $"DirY: {dirY}";
    }

    // Sets the player's respawn positioning
    public void RespawnPositioning()
    {
        gravityState = 0;
        rb.gravityScale = 1.25f;
        sprite.flipX = gravityState == 1;
    }
#endregion

}
#endregion
