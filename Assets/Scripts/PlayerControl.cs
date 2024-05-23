using System.Collections;
using UnityEngine;
using Photon.Pun;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    float movX, movY;
    Rigidbody2D rb;
    PhotonView view;
    private Animator animator;
    public Transform attackPoint;
    public float attackRange = 2f;
    public LayerMask enemyLayers;
    private TimeoutManager timeoutManager;
    private int health = 100;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        timeoutManager = gameObject.AddComponent<TimeoutManager>();
    }

    void Update()
    {
        if (view.IsMine)
        {
            movX = Input.GetAxisRaw("Horizontal");
            movY = Input.GetAxisRaw("Vertical");
            Vector2 movement = new Vector2(movX, movY);
            movement.Normalize();
            rb.velocity = movement * speed;
            Vector3 theScale = transform.localScale;
            if ((movX > 0 && theScale.x > 0) || (movX < 0 && theScale.x < 0))
            {
                theScale.x *= -1;
            }
            transform.localScale = theScale;
            if (Input.GetKeyDown(KeyCode.F))
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        if (view.IsMine)
        {

            animator.SetTrigger("Attack");
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in enemiesHit)
            {
                timeoutManager.SetTimeout(() => enemy.GetComponent<EnemyController>().TakeDamage(), 0.226f);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!view.IsMine)
        {
            rb.velocity = Vector2.zero;
        }
        if (other.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }
        if (other.gameObject.layer == 23)
        {
            TakeDamage();
        }

    }

    private void TakeDamage()
    {
        health -= 10;
        if (health <= 0)
        {
            GameObject Health = GameObject.Find("HealthValue");
            Health.GetComponent<Health>().SetHealth(0);
            Die();
        }
    }

    private void Die()
    {
        if (view.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (view.IsMine)
        {
            GameObject SpawnPlayers = GameObject.Find("SpawnPlayers");
            SpawnPlayers.GetComponent<SpawnPlayers>().SpawnPlayerAfterDeath();
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
