using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Ground
{
    [SerializeField] float rushTime;
    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Rush());
    }

    private IEnumerator Rush()
    {
        while(true)
        {
            animator.SetTrigger("Run");
            if(player.position.x > transform.position.x)
                rb2d.velocity = Vector2.right.normalized * speed;
            else
                rb2d.velocity = Vector2.left.normalized * speed;
            yield return new WaitForSeconds(rushTime);
        }
    }
}
