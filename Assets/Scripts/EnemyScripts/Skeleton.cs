using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{

    public int currentHealth;
    public int healthMax = 3;
    public float moveSpeed = 1f;
    public int attackDamage = 1;

    private Animator animEnemy;

    public bool isDead;

    [SerializeField] private bool dirDerecha;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = healthMax;
        animEnemy = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        //play hurt animation

        if (currentHealth <= 0)
        {
            isDead = true;
            GetComponent<RespawnEnemy>().statusDead = true;
            Die();
        }
                
    }


    void Die()
    {
        moveSpeed = 0;        
        animEnemy.SetBool("IsDead", true);  // die animation        
        GetComponent<BoxCollider2D>().enabled = false;        
        StartCoroutine("TimerDespawn");
        rb.isKinematic = true;
    }

    IEnumerator TimerDespawn()
    {
        yield return new WaitForSeconds(1.7f);
        gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        MachineState();

        if (currentHealth > 0)
        {
            isDead = false;
            GetComponent<RespawnEnemy>().statusDead = false;
            moveSpeed = 1;
            rb.isKinematic = false;
        }

        if (dirDerecha)
        {
            rb.velocity = new Vector2(moveSpeed + rb.velocity.x * Time.deltaTime, rb.velocity.y);
            transform.localScale = new Vector3(-.7f, .7f, 1);
        }
        else if (!dirDerecha)
        {
            rb.velocity = new Vector2(-moveSpeed + rb.velocity.x * Time.deltaTime, rb.velocity.y);
            transform.localScale = new Vector3(.7f, .7f, 1);
        }
    }


    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {            
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
    }


    private void MachineState()
    {
        if (dirDerecha && !isDead)
        {
            StartCoroutine("ChangeLookB");
        }
        else if (!dirDerecha && !isDead)
        {
            StartCoroutine("ChangeLook");
        }
    }

    IEnumerator ChangeLook()
    {
        yield return new WaitForSeconds(3f);
        dirDerecha = true;
    }

    IEnumerator ChangeLookB()
    {
        yield return new WaitForSeconds(3f);
        dirDerecha = false;
    }
}
