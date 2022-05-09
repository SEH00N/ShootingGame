using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : Ground
{
    protected Vector3 randPos;
    protected Vector2 targetPos;
    protected Vector2 target;
    protected float angle;
    [SerializeField] GameObject gunnerBullet;
    [SerializeField] Transform gunnerFirePos;
    [SerializeField] float fireDelay;
    [SerializeField] float fireinterval;

    protected override void OnEnable()
    {
        base.OnEnable();
        randPos = new Vector3(Random.Range(-13, 13), transform.position.y);
        StartCoroutine(Fire());
    }

    protected override void Update()
    {
        base.Update();
        if (state != State.Damaged)
            Positioning();
        targeting();
    }

    protected virtual void Positioning()
    {
        targetPos = randPos - transform.position;
        Vector2 dir = new Vector2(targetPos.x, rb2d.velocity.y);
        rb2d.velocity = dir * speed;
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
