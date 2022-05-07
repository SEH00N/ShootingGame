using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    public static Monster Instance = null;

    public enum State
    {
        Idle = 0,
        Move,
        Damaged,
    }

    public State state = State.Idle;

    [SerializeField] protected Transform player;

    public float monsterDamage = 1f;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (Instance == null)
            Instance = this;
        GameManager.Instance = FindObjectOfType<GameManager>();
        rb2d.gravityScale = 10f;
    }

    protected virtual void Update()
    {
        EntityDeSpawn();
        CharacterDir();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (((other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Player")))
        && state != State.Damaged)
        {
            state = State.Damaged;
            hp -= Player.Instance.playerDamage;
            NockBack();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Melee") && state != State.Damaged)
        {
            state = State.Damaged;
            hp -= Player.Instance.playerDamage;
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
        if (transform.rotation.y == 0)
            rb2d.AddForce(new Vector2(-20, 0), ForceMode2D.Impulse);
        else
            rb2d.AddForce(new Vector2(20, 0), ForceMode2D.Impulse);
        StartCoroutine(StateMove());
    }
}
