using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Controls playerControls;

    [SerializeField]
    private float speed, jumpForce;

    [SerializeField]
    private Rigidbody2D rb;

    private Vector2 movement;

    private float distToGround;

    private bool isGrounded;

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
        rb = gameObject.GetComponent<Rigidbody2D>();

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

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void Flip()
    {
        if (rb.velocity.x > 0f)
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        if (rb.velocity.x < 0f)
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    void Jump()
    {
        Debug.Log("Jump");

        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
