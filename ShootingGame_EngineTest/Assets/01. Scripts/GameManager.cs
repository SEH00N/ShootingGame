using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public enum Difficulty
    {
        begginer = 0,
        easy,
        normal,
        hard,
        veryHard,
        extreme,
    }

    public Difficulty difficulty = Difficulty.begginer;

    public Transform EntityPooling{ get; private set; }
    public Transform BulletPooling{ get; private set; }
    public Transform GunnerBulletPooling{ get; private set; }

    public Transform minPos;
    public Transform maxPos;

    public float elapsedTime;

    private void Start()
    {
        if(Instance == null)
            Instance = this;
        BulletPooling = GameObject.Find("BulletPooling").transform;
        EntityPooling = GameObject.Find("EntityPooling").transform;
        GunnerBulletPooling = GameObject.Find("GunnerBulletPooling").transform;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        GameDifficulty();
    }

    private void GameDifficulty()
    {
        switch(Math.Truncate(elapsedTime / 300))
        {
            case 0:
                difficulty = Difficulty.begginer;
                break;
            case 1:
                difficulty = Difficulty.easy;
                break;
            case 2:
                difficulty = Difficulty.normal;
                break;
            case 3:
                difficulty = Difficulty.hard;
                break;
            case 4:
                difficulty = Difficulty.veryHard;
                break;
            case 5:
                difficulty = Difficulty.extreme;
                break;
        }
    }
}
