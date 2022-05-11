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
        StopCoroutine(base.Positioning());
        StartCoroutine(Positioning());
    }

    protected override void Update()
    {
        base.Update();
        Positioning();
    }

    protected override IEnumerator Positioning()
    {
        targetPos = randPos - transform.position;
        while((onRight && transform.position.x <= randPos.x) || (!onRight && transform.position.x >= randPos.x))
        {
            rb2d.velocity = targetPos.normalized * speed;
            yield return 0;
        }
        yield return 0;
    }

}
