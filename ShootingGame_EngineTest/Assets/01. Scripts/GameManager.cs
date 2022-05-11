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
    }

    private IEnumerator ChangeDifficult()
    {
        while(true)
        {
            yield return new WaitForSeconds(300f);
            difficulty += 1;
            StartCoroutine(SpawnGolems());
        }
    }

    private IEnumerator SpawnGolems()
    {
        if(!isGolem)
            while(true)
            {
                isGolem = true;
                spawnCount++;
                SpawnManager.Instance.StopMethod();
                StartCoroutine(SpawnManager.Instance.SpawnGolems(spawnCount));
                yield return new WaitForSeconds(300);
            }
    }
}
