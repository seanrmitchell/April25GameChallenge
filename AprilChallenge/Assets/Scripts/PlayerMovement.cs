using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Controls playerControls;

    [SerializeField]
    private float speed, jumpForce, smoothMove;

    [SerializeField]
    private Rigidbody2D player;

    private Vector2 movement;

    private float distToGround;

    private bool isGrounded;

    private Vector2 baseVel = Vector2.zero;

    private void Awake()
    {
        playerControls = new Controls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDiable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Rigidbody2D>();

        distToGround = gameObject.GetComponent<Collider2D>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        Flip();

        playerControls.Movement.Jump.performed += _ => Jump();
    }

    private void FixedUpdate()
    {

        movement = playerControls.Movement.Strafe.ReadValue<Vector2>();

        if (movement.x > 0.1 || movement.x < -0.1)
        {

            Vector2 targetVelocity = new Vector2(movement.x * speed, player.velocity.y);
            player.velocity = Vector2.SmoothDamp(player.velocity, targetVelocity, ref baseVel, smoothMove);
        }
    }

    void Flip()
    {
        if (player.velocity.x > 0f)
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        if (player.velocity.x < 0f)
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    void Jump()
    {
        Debug.Log("Jump");

        if (isGrounded)
        {
            player.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D hit)
    {
        isGrounded = false;
    }


}
