using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // * Public
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; }}
    // * Parameters
    [SerializeField] private float moveSpeed = 1f;
    
    // * References
    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

   
    // * State
    private bool facingLeft = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void Update()
    {
        PlayerInput();
    }

    void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    void PlayerInput () {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);
    }

    void Move () {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    void AdjustPlayerFacingDirection () {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        facingLeft = mousePos.x < playerScreenPoint.x;
        spriteRenderer.flipX = facingLeft;
    }
}
