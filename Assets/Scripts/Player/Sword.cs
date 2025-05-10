using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // * Parameters
    [SerializeField] GameObject slashAnimPrefab;
    [SerializeField] Transform slashAnimSpawnPoint;
    [SerializeField] Transform weaponCollider;
    [SerializeField] float swordAttackCD = 0.5f;
    // * Refs
    private PlayerControls playerControls;
    private Animator animator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    // * Variable
    private GameObject slashAnim;
    private bool attackButtonDown, isAttacking = false;

    void Awake()
    {
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();

        activeWeapon = GetComponentInParent<ActiveWeapon>();
        playerController = GetComponentInParent<PlayerController>();
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    void Update()
    {
        MouseFollowWithOffset();
        Attack();
    }

    void StartAttacking() {
        attackButtonDown = true;
    }
    void StopAttacking () {
        attackButtonDown = false;
    }

    void Attack () {
        if (!attackButtonDown || isAttacking) return;

        animator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);

        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
        // StartCoroutine(AttackCDRoutine());
    }

    // IEnumerator AttackCD

    public void DoneAttackingAnimEvent () {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent () {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if(playerController.FacingLeft) {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent () {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if(playerController.FacingLeft) {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void MouseFollowWithOffset () {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if(mousePos.x < playerScreenPoint.x) {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
