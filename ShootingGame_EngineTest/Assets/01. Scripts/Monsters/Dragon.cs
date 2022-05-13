using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Monster
{
    public Vector3 randPos;
    private Vector2 targetPos;
    private Vector2 target;
    private float angle;
    private bool onRight;
    private bool onUp;
    [SerializeField] GameObject gunnerBullet;
    [SerializeField] Transform gunnerFirePos;
    [SerializeField] float fireDelay;
    [SerializeField] float fireinterval;

    protected override void OnEnable()
    {
        base.OnEnable();
        randPos = new Vector2(Random.Range(-13f, 13f), Random.Range(0, GameManager.Instance.maxPos.position.y - 3));
        rb2d.gravityScale = 0;
        StartCoroutine(Fire());
        RightFromTarget();
        UpFromTarget();
        StartCoroutine(PositioningY());
        StartCoroutine(PositioningX());
    }

    protected override void Update()
    {
        base.Update();
        targeting();
    }

    private IEnumerator PositioningX()
    {
        targetPos = randPos - transform.position;
        while(((onRight && transform.position.x <= randPos.x) || (!onRight && transform.position.x >= randPos.x)) && state != State.Damaged)
        {
            Vector2 dir = new Vector2(targetPos.x, rb2d.velocity.y);
            rb2d.velocity = dir.normalized * speed;
            yield return null;
        }
        yield return null;
    }

    private IEnumerator PositioningY()
    {
        targetPos = randPos - transform.position;
        while(((onUp && transform.position.y <= randPos.y) || (!onUp && transform.position.y >= randPos.y)) && state != State.Damaged)
        {
            Vector2 dir = new Vector2(rb2d.velocity.x, targetPos.y);
            rb2d.velocity = dir.normalized * speed;
            yield return null;
        }
        yield return null;
    }

    private bool RightFromTarget()
    {
        if(randPos.x > transform.position.x) return onRight = true;
        else return onRight = false;
    }

    private bool UpFromTarget()
    {
        if(randPos.y > transform.position.y) return onUp = true;
        else return onUp = false;
    }

    private void targeting()
    {
        Quaternion rotate = transform.rotation;
        target = player.position - transform.position;
        angle = Vector2.SignedAngle(Vector2.right, target);
        if (player.position.x > transform.position.x)
            transform.rotation = Quaternion.Euler(rotate.x, 0, rotate.z);
        else
            transform.rotation = Quaternion.Euler(rotate.x, -180, rotate.z);
    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            if (GameManager.Instance.GunnerBulletPooling.transform.childCount > 3)
                for (int i = 0; i < 3; i++)
                {
                    Transform bullet = GameManager.Instance.GunnerBulletPooling.GetChild(0);
                    bullet.SetParent(null);
                    bullet.gameObject.SetActive(true);
                    bullet.rotation = Quaternion.Euler(0, 0, angle);
                    bullet.position = gunnerFirePos.position;
                    yield return new WaitForSeconds(fireinterval);
                }
            else
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(gunnerBullet, gunnerFirePos.position, Quaternion.Euler(0, 0, angle));
                    yield return new WaitForSeconds(fireinterval);
                }
            yield return new WaitForSeconds(fireDelay);
        }
    }
}