using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Ground
{
    protected override void Update()
    {
        CharacterDir();
        Blocking();
    }

    private void OnDisable()
    {
        if(!SpawnManager.Instance.isSpawning && SpawnManager.Instance.bossEntities.childCount == 0)
            SpawnManager.Instance.StartMethod();
    }
}
