using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PwrUp : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            if(Player.Instance.hp <= 17)
                Player.Instance.hp += 3;
            else
                Player.Instance.hp = 20;
            if(Player.Instance.playerDamage < 5)
                Player.Instance.playerDamage += 1;
        }
    }
}
