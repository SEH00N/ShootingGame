using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    protected override void Update()
    {
        base.Update();
        if(state != State.Damaged)
            Followplayer();
    }

    private void Followplayer()
    {
        Vector2 target = player.position - transform.position;
        Vector2 dir = new Vector2(target.x, rb2d.velocity.y);
        rb2d.velocity = dir * speed;
    }
}
