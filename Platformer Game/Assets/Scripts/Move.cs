using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] float jumpForce = 3;
    [SerializeField] ContactFilter2D groundFilter;
    Vector2 inputDir = Vector2.zero;
    CapsuleCollider2D coll;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sprite;
    Transform playerObject;
    int gravityMode = 0; 
    bool grounded = false;
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerObject = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = inputDir.x != 0;
        anim.SetBool("Run", isRunning);
        anim.SetBool("Grounded", grounded);
        if (gravityMode == 0)
        {
            groundFilter.minNormalAngle = 80;
            groundFilter.maxNormalAngle = 100;
            anim.SetFloat("DirY", rb.velocity.y);
            sprite.flipY = false;
        }
        else if (gravityMode == 1)
        {
            groundFilter.minNormalAngle = -80;
            groundFilter.maxNormalAngle = -100;
            anim.SetFloat("DirY", -(rb.velocity.y));
            Debug.Log(anim.GetFloat("DirY"));
            sprite.flipY = true;
        }
        grounded = coll.IsTouching(groundFilter);
        MoveObject();
        Jump();
        GravitySwitch();
    }

    private void MoveObject()
    {
        Vector2 newVelocity = rb.velocity;
        newVelocity.x = inputDir.x * speed;
        rb.velocity = newVelocity;
    }

    private void Jump()
    {
        if (jump && grounded)
        {
            jump = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            if (gravityMode == 0)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (gravityMode == 1)
            {
                rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    public void setMoveDir(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();

        bool flipX = inputDir.x < 0;
        if (sprite.flipX != flipX)
        {
            sprite.flipX = flipX;
        }
    }

    public void ActivateJump(InputAction.CallbackContext context)
    {
        jump = context.started;
    }

    public void GravitySwitch()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            switch (gravityMode)
            {
                case 0:
                    gravityMode = 1;
                    rb.gravityScale = -1.25f;
                    groundFilter.minNormalAngle = 80;
                    groundFilter.maxNormalAngle = 100;
                    anim.SetFloat("DirY", rb.velocity.y);
                    sprite.flipY = false;
                    break;
                case 1:
                    gravityMode = 0;
                    rb.gravityScale = 1.25f;
                    break;
                    groundFilter.minNormalAngle = -80;
                    groundFilter.maxNormalAngle = -100;
                    anim.SetFloat("DirY", -(rb.velocity.y));
                    Debug.Log(anim.GetFloat("DirY"));
                    sprite.flipY = true;
                default:
                    break;
            }
            Debug.Log($"Gravity Mode: {gravityMode}");
        }
    }
}
