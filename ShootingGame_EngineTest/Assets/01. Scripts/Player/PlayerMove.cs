using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Player
{
    [SerializeField] float jumpPwr = 5f;
    
    [SerializeField] LayerMask groundLayer;

    private int jumpCount = 0;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if(state != State.Damaged)
        {
            PlayerMovement();
            PlayerJumping();
        }
    }

    private void PlayerMovement()
    {
        state = State.Move;

        float h = Input.GetAxisRaw("Horizontal");
        Vector2 dir = new Vector2(h * speed, rb2d.velocity.y);
        rb2d.velocity = dir;
    }

    private void PlayerJumping()
    {
        if(Input.GetButtonDown("Jump") && jumpCount < 1)
        {
            jumpCount++;
            rb2d.AddForce(Vector2.up * jumpPwr, ForceMode2D.Impulse);
        }
        if(isGround())
            jumpCount = 0;
    }

    private bool isGround()
    {
        return Physics2D.OverlapBox(transform.position, col2d.bounds.size, 0, groundLayer);
    }
}
