using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    float movX, movY;
    Rigidbody2D rb;
    PhotonView view;
    Transform playerTransform;
    public Animator animator;
    private Collider col;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
        playerTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider>();
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
                animator.SetBool("IsAttacking", true);
            }
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("playerAttackSword"))
            {
                animator.SetBool("IsAttacking", false);
            }
        }
    }
}
