using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float hp = 10f;
    [SerializeField] GameObject pwrUp;

    protected Rigidbody2D rb2d;
    protected Collider2D col2d;

    public bool IsRight { get; private set; } = false;

    protected virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
    }
    
    public void CharacterDir()
    {
        if(rb2d.velocity.x >= 0.1f)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if(rb2d.velocity.x <= -0.1f)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void EntityDeSpawn()
    {
        if(hp <= 0.1f)
        {
            transform.SetParent(GameManager.Instance.EntityPooling);
            gameObject.SetActive(false);
            int randVal = Random.Range(0, 100);
            if(randVal > 95)
                Instantiate(pwrUp, transform.position, Quaternion.identity);
        }
    }
}
