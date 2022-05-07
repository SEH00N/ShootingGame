using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : Monster
{
    [SerializeField] float rushTime;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Rush());
    }

    private IEnumerator Rush()
    {
        while(true)
        {
            if(player.position.x > transform.position.x)
                rb2d.velocity = Vector2.right * speed;
            else
                rb2d.velocity = Vector2.left * speed;
            yield return new WaitForSeconds(rushTime);
        }
    }
}
