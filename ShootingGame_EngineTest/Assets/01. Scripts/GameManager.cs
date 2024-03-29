using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public enum Difficulty
    {
        beginner = 0,
        easy,
        normal,
        hard,
        veryHard,
        extreme,
    }

    public Difficulty difficulty = Difficulty.beginner;

    public Transform BossEntityPooling;
    public Transform EntityPooling;
    public Transform BulletPooling;
    public Transform GunnerBulletPooling;
    public Transform MonsterAttackPooling;
    public Transform DragonProjectilePooling;
    public Transform minPos;
    public Transform maxPos;

    public float elapsedTime;
    public int spawnCount = 0;

    public bool isGolem = false;
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
            StartCoroutine(ChangeDifficult());
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // if(Input.GetKey(KeyCode.LeftShift))
        // {
        //     if(Input.GetKeyDown(KeyCode.T))
        //         Time.timeScale = 5;
        //     else if(Input.GetKeyUp(KeyCode.T))
        //         Time.timeScale = 1;

        //     if(Input.GetKeyDown(KeyCode.P))
        //     {
        //         Player.Instance.hp = Mathf.Min(Player.Instance.hp + 5, 20);
        //         Player.Instance.playerDamage += 1;
        //     }
        // }
    }

    private IEnumerator ChangeDifficult()
    {
        while(true)
        {
            yield return new WaitForSeconds(300f);
            difficulty += 1;
            StartCoroutine(SpawnMushroom());
        }
    }

    private IEnumerator SpawnMushroom()
    {
        if(!isGolem)
        {
            isGolem = true;
            spawnCount++;
            SpawnManager.Instance.StopMethod();
            StartCoroutine(SpawnManager.Instance.SpawnMushroom(spawnCount));
            yield return null;
        }
    }
}
