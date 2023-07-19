using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private int auxDirecao;
    [SerializeField] private LayerMask jumpableGround;

    private SpriteRenderer spriteRenderer;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 9f;

    private enum MovementState
    {
        ide, running, jump, falling
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        HandheldPCInput();

    }
    private void HandheldPCInput()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jumps();
        }
        if (auxDirecao != 0)
        {
            transform.Translate(moveSpeed * Time.deltaTime * auxDirecao, 0, 0);
        }
        UpdateAnimation();
    }
    public void Jumps()
    {
        AudioManager.instance.Play("Jump");
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    private void UpdateAnimation()
    {
        MovementState state;

        if (dirX > 0f && auxDirecao > 0)
        {
            state = MovementState.running;
            spriteRenderer.flipX = false;

        }
        else if (dirX < 0f && auxDirecao < 0)
        {
            state = MovementState.running;

            spriteRenderer.flipX = true;
        }
        else
        {
            state = MovementState.ide;

        }
        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;

        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
    public void MobileMove(int move)
    {
        auxDirecao = move;
    }
}
