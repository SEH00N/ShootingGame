using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Ground
{
    [SerializeField] GameObject attack;

    private Queue<GameObject> attackPool = new Queue<GameObject>();
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        if(state != State.Damaged && state != State.Attack)
            Followplayer();
    }

    protected virtual void LateUpdate()
    {
        if(state != State.Attack)
            StartCoroutine(Attack(5f, 1.7f));
    }

    private void Followplayer()
    {
        state = State.Move;
        Vector2 target = player.position - transform.position;
        Vector2 dir = new Vector2(target.x, 0);
        rb2d.velocity = dir.normalized * speed;
    }

    protected IEnumerator Attack(float near, float distance)
    {
        Vector2 pos = this.transform.position;
        if(isNear(near) && state == State.Move)
        {
            InstantiateOrPool();
            GameObject temp = attackPool.Dequeue();
            animator.SetTrigger("Attack");
            state = State.Attack;
            if(pos.x > player.position.x)
                temp.transform.position = new Vector3(transform.position.x - distance, transform.position.y);
            else temp.transform.position = new Vector3(transform.position.x + distance, transform.position.y);
            yield return new WaitForSeconds(0.5f);
            temp.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            temp.SetActive(false);
            yield return new WaitForSeconds(0.7f);
            state = State.Move;
        }
    }

    private void InstantiateOrPool()
    {
        if(GameManager.Instance.MonsterAttackPooling.childCount < 1)
        {
            GameObject temp = Instantiate(attack, transform.position, Quaternion.identity);
            temp.transform.SetParent(GameManager.Instance.MonsterAttackPooling);
            attackPool.Enqueue(temp);
        }
        else
            attackPool.Enqueue(GameManager.Instance.MonsterAttackPooling.GetChild(0).gameObject);
    }
}
