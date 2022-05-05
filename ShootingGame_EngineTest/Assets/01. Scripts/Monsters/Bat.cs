using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Monster
{
    [SerializeField] Transform player;

    protected override void Update()
    {
        base.Update();
        if(state != State.Damaged)
            Followplayer();
    }

    private void Followplayer()
    {
        Vector2 target = player.position - transform.position;

        rb2d.velocity = target * speed;
    }

}
