using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    [SerializeField] Text difficulty;

    private void Update()
    {
        difficulty.text = $"{GameManager.Instance.difficulty.ToString().ToUpper()}";
    }
}
