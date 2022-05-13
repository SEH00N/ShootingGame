using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] Transform player;

    private void Update()
    {
        MeleeDir();
    }

    private void MeleeDir()
    {
        Vector3 pos = new Vector3(player.position.x, player.position.y + 2);
        transform.position = pos;
        transform.rotation = player.transform.rotation;
    }
}
