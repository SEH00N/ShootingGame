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
        if(state != State.Attack)
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
        Vector2 pos = this.transform.position;
        if(isNear(5) && state == State.Move)
        {
            InstantiateOrPool();
            GameObject temp = attackPool.Dequeue();
            animator.SetTrigger("Attack");
            state = State.Attack;
            if(pos.x > player.position.x)
                temp.transform.position = new Vector3(transform.position.x - 1.7f, transform.position.y);
            else temp.transform.position = new Vector3(transform.position.x + 1.7f, transform.position.y);
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
        if(GameManager.Instance.SkeletonAttackPooling.childCount < 1)
        {
            GameObject temp = Instantiate(attack, transform.position, Quaternion.identity);
            temp.transform.SetParent(GameManager.Instance.SkeletonAttackPooling);
            attackPool.Enqueue(temp);
        }
        else
            attackPool.Enqueue(GameManager.Instance.SkeletonAttackPooling.GetChild(0).gameObject);
    }
}
