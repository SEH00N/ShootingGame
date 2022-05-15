using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonProjectileMove : GunnerBulletMove
{
    protected override void DeSpawn()
    {
        transform.SetParent(GameManager.Instance.DragonProjectilePooling);
        gameObject.SetActive(false);
    }
}
