using System.Resources;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player Instance = null;

    public enum State
    {
        Idle = 0,
        Move,
        Magic,
        Melee,
        Damaged,
        Untouched,
    }

    public State state = State.Idle;

    public float playerDamage = 1f;

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    protected virtual void Update()
    {
        EntityDeSpawn();
        CharacterDir();
        if(rb2d.velocity.x == 0)
            state = State.Move;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Monster") || other.gameObject.CompareTag("Meteor") 
           ||other.gameObject.CompareTag("Goblin") || other.gameObject.CompareTag("Attack"))&& state != State.Damaged)
        {
            state = State.Damaged;
            hp -= Monster.Instance.monsterDamage;
            StartCoroutine(NockBack(10));
        }
    }

    private IEnumerator NockBack(float pwr)
    {
        if(rb2d.velocity.x < 0)
            rb2d.AddForce(new Vector2(-pwr, 0), ForceMode2D.Impulse);
        else if(rb2d.velocity.x > 0)
            rb2d.AddForce(new Vector2(pwr, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        state = State.Move;
    }
}
