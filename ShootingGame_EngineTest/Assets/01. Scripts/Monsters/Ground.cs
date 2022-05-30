using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : Monster
{
    protected override void OnEnable()
    {
        base.OnEnable();
        if(transform.position.x > 0)
            transform.position = new Vector3(transform.position.x, GameManager.Instance.minPos.position.y + 6);
        else transform.position = new Vector3(transform.position.x, GameManager.Instance.minPos.position.y + 2);
    }
}
