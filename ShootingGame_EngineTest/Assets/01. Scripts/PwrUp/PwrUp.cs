using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PwrUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Player.Instance.hp += 3;
            Player.Instance.playerDamage += 1;
        }
    }
}
