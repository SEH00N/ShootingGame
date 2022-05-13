using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Player
{
    new public static PlayerMove Instance = null;

    [SerializeField] float jumpPwr = 13f;
    
    [SerializeField] LayerMask groundLayer;

    private int jumpCount = 0;

    protected override void Awake()
    {
        base.Awake();
        if(Instance == null)
            Instance = this;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if(state != State.Damaged && state != State.Melee)
        {
            PlayerMovement();
            PlayerJumping();
        }
            PlayerDash();
    }

    private void PlayerMovement()
    {
        state = State.Move;
        float h = Input.GetAxisRaw("Horizontal");
        Vector2 dir = new Vector2(h * speed, rb2d.velocity.y);
        rb2d.velocity = dir;
        Vector3 limit = new Vector3(Mathf.Clamp(transform.position.x, 
        GameManager.Instance.minPos.position.x + 6.5f, GameManager.Instance.maxPos.position.x - 6.5f), transform.position.y);

        transform.position = limit;

        if(!(Mathf.Abs(rb2d.velocity.x) > 0.1))
            animator.SetTrigger("Idle");
        else if(!(Mathf.Abs(rb2d.velocity.x) <= 0.1))
            animator.SetTrigger("Run");
    }

    private void PlayerJumping()
    {
        if(Input.GetButtonDown("Jump") && jumpCount < 1)
        {
            animator.SetTrigger("Jump");
            jumpCount++;
            rb2d.AddForce(Vector2.up * jumpPwr, ForceMode2D.Impulse);
        }
        if(isGround())
            jumpCount = 0;

        if(rb2d.velocity.y < 0)
            animator.SetTrigger("Fall");
    }

    private void PlayerDash()
    {
        if(Input.GetKey(KeyCode.LeftShift) && state != State.Melee)
        {
            animator.speed = 2;
            speed = 14f;
            jumpPwr = 16f;
        }
        else
        {
            animator.speed = 1;
            speed = 7f;
            jumpPwr = 13f;
        }
    }

    public bool isGround()
    {
        if(Physics2D.OverlapBox(transform.position, col2d.bounds.size, 0, groundLayer))
        {
            if(state != State.Move)
                animator.SetTrigger("Idle");
            return true;
        }
        else return false;
    }
}
