using System;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTime : MonoBehaviour
{
    [SerializeField] Text playTime;

    private double min;
    private double sec;

    private void Update()
    {
        ElapsedTime();
        TimeTyping();
    }

    private void ElapsedTime()
    {
        min = Math.Truncate(GameManager.Instance.elapsedTime / 60);
        sec = Math.Truncate(GameManager.Instance.elapsedTime % 60);
    }

    private void TimeTyping()
    {
        playTime.text = $"{min}:{sec}";
    }
}
