using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] float speed = 3;
    Vector2 inputDir = Vector2.zero;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        rb.velocity = new Vector2(inputDir.x * speed, rb.velocity.y);
    }

    public void setMoveDir (InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();
    }
}
