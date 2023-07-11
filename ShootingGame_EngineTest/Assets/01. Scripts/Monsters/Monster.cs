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
        Attack,
    }

    public State state = State.Idle;

    [SerializeField] protected Transform player;
    [SerializeField] protected LayerMask palyerLayer;

    public float monsterDamage = 1f;

    private float nockBackPwr = 10f;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (Instance == null)
            Instance = this;
        GameManager.Instance = FindObjectOfType<GameManager>();
        state = State.Idle;
        rb2d.gravityScale = 15;
        SetStartPos();
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
            StartCoroutine(NockBack(nockBackPwr));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Melee") && state != State.Damaged)
        {
            state = State.Damaged;
            hp -= Player.Instance.playerDamage;
            StartCoroutine(NockBack(nockBackPwr * 2));
        }
    }

    private IEnumerator NockBack(float pwr)
    {
        if (transform.rotation.y == 0)
            rb2d.AddForce(new Vector2(-pwr, 0), ForceMode2D.Impulse);
        else
            rb2d.AddForce(new Vector2(pwr, 0), ForceMode2D.Impulse);
        yield return new WaitForSecondsRealtime(0.5f);
        state = State.Move;
    }

    private void SetStartPos()
    {
        Vector2 min = GameManager.Instance.minPos.position;
        Vector2 max = GameManager.Instance.maxPos.position;
        Vector2 startPos;
        int temp = Random.Range(0, 2);
        if(temp == 0)
            startPos = new Vector2(min.x, Random.Range(min.y + 3f, max.y));
        else
            startPos = new Vector2(max.x, Random.Range(min.y + 3f, max.y));

        transform.position = startPos;
    }

    public void Blocking()
    {
        Vector3 limit = new Vector3(Mathf.Clamp(transform.position.x, 
        GameManager.Instance.minPos.position.x + 7f, GameManager.Instance.maxPos.position.x - 7f), transform.position.y);

        transform.position = limit;
    }

    protected bool isNear(float distance)
    {
        Vector2 col2dSize = col2d.bounds.size;
        Vector2 size = new Vector2(col2dSize.x + distance, col2dSize.y);
        return Physics2D.OverlapBox(transform.position, size, 0, palyerLayer);
    }
}
