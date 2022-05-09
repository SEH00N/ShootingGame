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

    public Transform EntityPooling;
    public Transform BulletPooling;
    public Transform GunnerBulletPooling;

    public Transform minPos;
    public Transform maxPos;

    public float elapsedTime;
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
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
                difficulty = Difficulty.beginner;
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
