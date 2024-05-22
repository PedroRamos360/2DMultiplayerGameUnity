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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
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
        }
    }
}
