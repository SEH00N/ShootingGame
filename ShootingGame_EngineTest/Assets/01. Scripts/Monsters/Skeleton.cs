using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Ground
{
    [SerializeField] GameObject attack;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();
        if(state != State.Damaged && state != State.Attack)
            Followplayer();
    }

    private void LateUpdate()
    {
        StartCoroutine(Attack());
    }

    private void Followplayer()
    {
        state = State.Move;
        Vector2 target = player.position - transform.position;
        Vector2 dir = new Vector2(target.x, rb2d.velocity.y);
        rb2d.velocity = dir.normalized * speed;
    }

    private IEnumerator Attack()
    {
        if(isNear(5) && state == State.Move)
        {
            animator.SetTrigger("Attack");
            state = State.Attack;
            yield return new WaitForSeconds(0.8f);
            attack.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            attack.SetActive(false);
            yield return new WaitForSeconds(0.7f);
            state = State.Move;
        }
    }
}
