using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : Monster
{
    Vector3 randPos;

    protected override void Start()
    {
        base.Start();
        randPos = new Vector3(Random.Range(-13, 13), transform.position.y);
    }

    protected override void Update()
    {
        base.Update();
        Positioning();
    }

    private void Positioning()
    {
        Vector2 target = randPos - transform.position;
        Vector2 dir = new Vector2(target.x, rb2d.velocity.y);
        rb2d.velocity = dir * speed;
    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(3f);
    }
}
