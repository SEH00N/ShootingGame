using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Skeleton
{
    protected override void LateUpdate()
    {
        if(state != State.Attack)
            StartCoroutine(Attack(3f, 3f));
    }

    private void OnDisable()
    {
        if(!SpawnManager.Instance.isSpawning && SpawnManager.Instance.bossEntities.childCount == 0)
            SpawnManager.Instance.StartMethod();
    }
}
