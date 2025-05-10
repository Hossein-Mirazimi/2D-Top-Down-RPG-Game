using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // * Public
    public bool FacingLeft { get { return facingLeft; } }
    public static PlayerController Instance;
    // * Parameters
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    
    // * References
    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

   
    // * State
    private float startingMoveSpeed;
    private bool facingLeft = false;
    private bool isDashing = false;

    void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
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

    void Dash () {
        if (isDashing) return;

        isDashing = true;
        moveSpeed *= dashSpeed;
        myTrailRenderer.emitting = true;
        StartCoroutine(EndDashRoutine());
    }

    IEnumerator EndDashRoutine () {
        float dashTime = 0.2f;
        float dashCD = 0.25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
