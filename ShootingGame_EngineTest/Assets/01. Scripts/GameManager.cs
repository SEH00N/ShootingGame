using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public Transform EntityPooling{ get; private set; }
    public Transform bulletPooling{ get; private set; }

    public Transform minPos;
    public Transform maxPos;

    private void Start()
    {
        if(Instance == null)
            Instance = this;
        bulletPooling = GameObject.Find("BulletPooling").transform;
        EntityPooling = GameObject.Find("EntityPooling").transform;
    }
}
