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
        Fire,
        Damaged,
    }

    public State state = State.Idle;

    public float playerDamage = 1f;

    protected override void Start()
    {
        base.Start();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            state = State.Damaged;
            hp -= Monster.Instance.monsterDamage;
            NockBack();
        }
    }

    private IEnumerator StateMove()
    {
        yield return new WaitForSeconds(0.5f);
        state = State.Move;
    }

    private void NockBack()
    {
        if(rb2d.velocity.x < 0)
            rb2d.AddForce(new Vector2(10, 0), ForceMode2D.Impulse);
        else if(rb2d.velocity.x > 0.1f)
            rb2d.AddForce(new Vector2(-10, 0), ForceMode2D.Impulse);
        StartCoroutine(StateMove());
    }
}
