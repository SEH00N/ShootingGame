using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    private void Update()
    {
        BulletMovement();
        Limit();
    }

    private void BulletMovement()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void Limit()
    {
        if(transform.position.x > GameManager.Instance.maxPos.position.x ||
           transform.position.x < GameManager.Instance.minPos.position.x)
            DeSpawn();
    }

    private void DeSpawn()
    {
        transform.SetParent(GameManager.Instance.bulletPooling);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Monster"))
            DeSpawn();
    }
}
