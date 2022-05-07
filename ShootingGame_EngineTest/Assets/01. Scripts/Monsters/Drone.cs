using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Gunner
{
    protected override void OnEnable()
    {
        base.OnEnable();
        randPos = new Vector3(Random.Range(GameManager.Instance.minPos.position.x + 8f, GameManager.Instance.maxPos.position.x - 8f),
         Random.Range(0, GameManager.Instance.maxPos.position.y - 3f), 0);
        rb2d.gravityScale = 0f;
    }

    protected override void Update()
    {
        base.Update();
        Positioning();
    }

    protected override void Positioning()
    {
        targetPos = randPos - transform.position;
        rb2d.velocity = targetPos * speed;
    }
}
