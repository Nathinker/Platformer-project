using UnityEditor.Experimental.GraphView;
using UnityEditor.Timeline.Actions;
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
    bool grounded = false;
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Run", inputDir.x != 0);
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("DirY", rb.velocity.y);
        grounded = coll.IsTouching(groundFilter);
        MoveObject();
        Jump();
    }

    private void MoveObject()
    {
        rb.velocity = new Vector2(inputDir.x * speed, rb.velocity.y);
    }

    private void Jump()
    {
        if (jump && grounded)
        {
            jump = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void setMoveDir (InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();

        if (inputDir.x > 0 && sprite.flipX)
        {
            sprite.flipX = false;
        }
        if (inputDir.x < 0 && !sprite.flipX)
        {
            sprite.flipX = true;
        }
    }

    public void ActivateJump (InputAction.CallbackContext context)
        {
            if (context.started)
            {
                jump = true;
            }
            if (context.performed || context.canceled)
            {
                jump = false;
            }
        }
}
