using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Character
{   
    protected override void OnEnable()
    {
        base.OnEnable();
        hp = 0;
        speed = Random.Range(2f, 10f);
        SetPos();
    }

    void Update()
    {
        MeteorDrop();
    }

    private void MeteorDrop()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EntityDeSpawn();
    }

    private void SetPos()
    {
        Vector2 min = GameManager.Instance.minPos.position;
        Vector2 max = GameManager.Instance.maxPos.position;

        Vector2 randPos = new Vector2(Random.Range(min.x + 5, max.x - 5), max.y);

        transform.position = randPos;
    }
}
