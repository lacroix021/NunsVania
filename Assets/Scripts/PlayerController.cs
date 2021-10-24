using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    [HideInInspector]
    public ControllerType controller;

    public float weaponState;

    [SerializeField] private float h;
    [SerializeField] private float v;


    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D capsColl;

    public Collider2D feetPos;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlatform;

    [Header("Valores Modificables")]
    public int currentHealth = 15;
    public int maxHealth = 15;
    public float hurtForceY;
    public float hurtForceX;
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float speedBase = 100f;
    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;


    [SerializeField] private float nextAttackTime = 0f;
    [SerializeField] private float attackRate = 2f;


    [Header("Pulsaciones de Botones")]
    [SerializeField] private bool RightKey = false;
    [SerializeField] private bool LeftKey = false;
    [SerializeField] private bool UpKey = false;
    [SerializeField] private bool DownKey = false;
    [SerializeField] private bool JumpKey = false;
    [SerializeField] private bool FireKey = false;

    [Header("Info Acciones")]
    [SerializeField] private bool isJump = false;
    [SerializeField] private bool isAttack = false;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isPlatformed = false;
    [SerializeField] private bool canJump = false;
    [SerializeField] private bool canMove = true;
    public bool isCrouched = false;


    private bool AttackAnim;
    private bool AirAttackAnim;
    private bool CrouchAttackAnim;
    private bool HurtAnim;

    public Joystick joystick;

    private bool isDead;

    Renderer rend;
    Color c;


    public bool dirDerecha;
    public float tiempo;
    public float miTiempo;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsColl = GetComponent<CapsuleCollider2D>();
        weaponState = 0f;
        rend = GetComponent<Renderer>();
        c = rend.material.color;
    }


    // Update is called once per frame
    void Update()
    {
        InputKeys();
        OnFloor();
        Animations();
        Attack();

        AttackAnim = anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
        AirAttackAnim = anim.GetCurrentAnimatorStateInfo(0).IsTag("AirAttack");
        CrouchAttackAnim = anim.GetCurrentAnimatorStateInfo(0).IsTag("CrouchAttack");
        HurtAnim = anim.GetCurrentAnimatorStateInfo(0).IsTag("Sonia_Hurt");
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
    }

    private void InputKeys()
    {
        if (controller == ControllerType.MOBILE)
        {
            h = joystick.Horizontal;
            v = joystick.Vertical;

            //Jump
            if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded && !isCrouched)
            {
                isJump = true;
            }

            //Attack
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                if (Time.time >= nextAttackTime)
                {
                    isAttack = true;
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
        else if (controller == ControllerType.PC)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");

            //Jump
            if (Input.GetButtonDown("Jump") && isGrounded && !isCrouched)
            {
                isJump = true;
            }

            //Attack key PC
            if (Input.GetButtonDown("Fire1"))
            {
                if (Time.time >= nextAttackTime)
                {
                    isAttack = true;
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }


        // Horizontal movement keys
        if (h >= 0.5f && !AttackAnim)
        {
            RightKey = true;
            LeftKey = false;
            transform.localScale = new Vector3(1, 1, 1);    //Flip right
        }
        else if (h >= 0f && h < 0.5f)
        {
            RightKey = false;
        }

        if (h <= -0.5f && !AttackAnim)
        {
            LeftKey = true;
            RightKey = false;
            transform.localScale = new Vector3(-1, 1, 1);   //Flip left
        }
        else if (h <= 0f && h > -0.5f)
        {
            LeftKey = false;
        }

        // Vertical movement keys
        if (v >= 0.65f)
        {
            UpKey = true;
            DownKey = false;
        }
        else if (v <= -0.6f)
        {
            DownKey = true;
            UpKey = false;
        }

        if (v > -0.6f && v < 0.65f)
        {
            DownKey = false;
        }

        if (v < 0.65f && v > -0.6f)
        {
            UpKey = false;
        }

        //CrouchKey
        if (DownKey && isGrounded)
        {
            isCrouched = true;
            moveSpeed = 0;
        }
        else if (!DownKey)
        {
            isCrouched = false;
            moveSpeed = speedBase;
        }
    }


    private void Movement()
    {
        if (RightKey && !AttackAnim && !CrouchAttackAnim && !HurtAnim && canMove)
        {
            rb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
        else if (LeftKey && !AttackAnim && !CrouchAttackAnim && !HurtAnim && canMove)
        {
            rb.velocity = new Vector2(-moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
        else if (!RightKey && !LeftKey && canMove)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }


    private void Jump()
    {
        if (isJump && !AttackAnim && canMove)
        {
            anim.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0, jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
            isJump = false;
            canJump = false;
        }

        if (controller == ControllerType.MOBILE)
        {
            //fourlines better jump
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !CrossPlatformInputManager.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }

        if (controller == ControllerType.PC)
        {
            //fourlines better jump
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }

    private void Attack()
    {
        if (isAttack && !isJump && isGrounded)
        {
            moveSpeed = 0;
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetTrigger("Attack");
            isAttack = false;
        }
        else if (isAttack && !isGrounded)
        {
            anim.SetTrigger("Attack");
            isAttack = false;
            StartCoroutine(WaitToGround());
        }
    }

    IEnumerator WaitToGround()
    {
        yield return new WaitUntil(() => isGrounded);
        if (AirAttackAnim)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            canMove = false;
            yield return new WaitForSeconds(0.15f);
            canMove = true;
        }
    }

    private void OnFloor()
    {
        isGrounded = Physics2D.IsTouchingLayers(feetPos, whatIsGround);
        isPlatformed = Physics2D.IsTouchingLayers(feetPos, whatIsPlatform);
    }


    private void Animations()
    {
        anim.SetFloat("VelX", Mathf.Abs(h));
        anim.SetBool("Crouched", isCrouched);
        anim.SetBool("Grounded", isGrounded);

        if (rb.velocity.y > 0 && !isGrounded)
        {
            feetPos.enabled = false;
        }
        else if (rb.velocity.y <= 0 && !isGrounded)
        {
            feetPos.enabled = true;
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        anim.SetTrigger("Hurt"); //play hurt animation

        if (currentHealth > 0)
        {
            StartCoroutine("GetInvulnerable");
        }

        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    IEnumerator GetInvulnerable()
    {
        Physics2D.IgnoreLayerCollision(12, 14, true);
        c.a = 0.5f;
        rend.material.color = c;
        yield return new WaitForSeconds(1.3f);
        Physics2D.IgnoreLayerCollision(12, 14, false);
        c.a = 1f;
        rend.material.color = c;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && currentHealth > 0 || collision.gameObject.tag == "Boss" && currentHealth > 0)
        {
            canMove = false;

            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(-hurtForceX, hurtForceY), ForceMode2D.Impulse);
            }
            else
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(hurtForceX, hurtForceY), ForceMode2D.Impulse);
            }

            StartCoroutine(RecoverMove());
        }
    }

    IEnumerator RecoverMove()
    {
        yield return new WaitForSeconds(0.2f);
        canMove = true;

    }
    void Die()
    {
        anim.SetBool("IsDead", true);  // die animation
        rb.velocity = new Vector2(0, rb.velocity.y);
        canMove = false;
        StartCoroutine("afterDie");
    }

    IEnumerator afterDie()
    {
        yield return new WaitForSeconds(1f);
        rb.isKinematic = true;
        capsColl.enabled = false;
        this.enabled = false;
    }
}
