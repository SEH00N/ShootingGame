using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : Monster
{
    Vector3 randPos;
    Vector2 target;
    [SerializeField] GameObject gunnerBullet;
    [SerializeField] Transform gunnerFirePos;
    [SerializeField] float fireDelay;

    protected override void Start()
    {
        base.Start();
        randPos = new Vector3(Random.Range(-13, 13), transform.position.y);
        StartCoroutine(Fire());
    }

    protected override void Update()
    {
        base.Update();
        Positioning();
        targeting();
    }

    private void Positioning()
    {
        target = randPos - transform.position;
        Vector2 dir = new Vector2(target.x, rb2d.velocity.y);
        rb2d.velocity = dir * speed;
    }

    private void targeting()
    {
        Quaternion rotate = transform.rotation;
        if (player.position.x > transform.position.x)
            transform.rotation = Quaternion.Euler(rotate.x, 0, rotate.z);
        else
            transform.rotation = Quaternion.Euler(rotate.x, -180, rotate.z);
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            if (GameManager.Instance.GunnerBulletPooling.transform.childCount > 0)
                for (int i = 0; i < 3; i++)
                {
                    Transform bullet = GameManager.Instance.GunnerBulletPooling.GetChild(0);
                    bullet.SetParent(null);
                    bullet.gameObject.SetActive(true);
                    bullet.rotation = gunnerFirePos.rotation;
                    bullet.position = gunnerFirePos.position;
                    yield return new WaitForSeconds(0.5f);
                }
            else
                for (int i = 0; i < 3; i++)
                {
                    target = player.position - transform.position;
                    float angle = Vector2.SignedAngle(Vector2.right, target);
                    Instantiate(gunnerBullet, gunnerFirePos.position, Quaternion.Euler(0, 0, angle));
                    yield return new WaitForSeconds(0.5f);
                }
            yield return new WaitForSeconds(fireDelay);
        }
    }
}
