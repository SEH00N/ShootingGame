using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    [SerializeField] Image hpGauge;

    void Update()
    {
        hpGauge.fillAmount = Player.Instance.hp / 20f;
    }
}
