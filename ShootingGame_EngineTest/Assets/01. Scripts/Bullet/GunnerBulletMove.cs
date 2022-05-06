using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerBulletMove : MonoBehaviour
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
        Vector2 pos = transform.position;
        if (pos.x > GameManager.Instance.maxPos.position.x ||
           pos.x < GameManager.Instance.minPos.position.x ||
           pos.y > GameManager.Instance.maxPos.position.y ||
           pos.y < GameManager.Instance.minPos.position.y)
            DeSpawn();
    }

    private void DeSpawn()
    {
        transform.SetParent(GameManager.Instance.GunnerBulletPooling);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            DeSpawn();
    }
}
