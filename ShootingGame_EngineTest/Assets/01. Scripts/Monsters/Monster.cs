using System.Drawing;
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

    private float nockBackPwr = 10f;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (Instance == null)
            Instance = this;
        GameManager.Instance = FindObjectOfType<GameManager>();
        state = State.Idle;
        rb2d.gravityScale = 10;
        SetStartPos();
        StartCoroutine(GetSpeed());
    }

    protected virtual void Update()
    {
        EntityDeSpawn();
        CharacterDir();
        Blocking();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (((other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Player")))
        && state != State.Damaged)
        {
            state = State.Damaged;
            hp -= Player.Instance.playerDamage;
            NockBack(nockBackPwr);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Melee") && state != State.Damaged)
        {
            state = State.Damaged;
            hp -= Player.Instance.playerDamage;
            NockBack(nockBackPwr * 2);
        }
    }

    private IEnumerator StateMove()
    {
        yield return new WaitForSeconds(0.5f);
        state = State.Move;
    }

    private void NockBack(float pwr)
    {
        if (transform.rotation.y == 0)
            rb2d.AddForce(new Vector2(-pwr, 0), ForceMode2D.Impulse);
        else
            rb2d.AddForce(new Vector2(pwr, 0), ForceMode2D.Impulse);
        StartCoroutine(StateMove());
    }

    private void SetStartPos()
    {
        Vector2 min = GameManager.Instance.minPos.position;
        Vector2 max = GameManager.Instance.maxPos.position;
        Vector2 startPos;
        int temp = Random.Range(0, 2);
        if(temp == 0)
            startPos = new Vector2(min.x, Random.Range(min.y, max.y));
        else
            startPos = new Vector2(max.x, Random.Range(min.y, max.y));

        transform.position = startPos;
    }

    private IEnumerator GetSpeed()
    {
        float endSpeed = speed;
        speed = 0;
        for(int i = 0; i < 10; i++)
        {
            speed += endSpeed / 10;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Blocking()
    {
        Vector3 limit = new Vector3(Mathf.Clamp(transform.position.x, 
        GameManager.Instance.minPos.position.x, GameManager.Instance.maxPos.position.x), transform.position.y);

        transform.position = limit;
    }
}
