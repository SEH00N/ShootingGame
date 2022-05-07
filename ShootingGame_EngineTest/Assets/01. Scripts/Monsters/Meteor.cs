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
    }

    void Update()
    {
        MeteorDrop();
    }

    private void MeteorDrop()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("123");
        EntityDeSpawn();
    }
}
