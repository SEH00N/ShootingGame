using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] Transform player;

    private void Update()
    {
        CharacterDir();
    }

    private void CharacterDir()
    {
        Vector2 pos = this.transform.position;
        if(pos.x >= player.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if(pos.x < player.position.x)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
