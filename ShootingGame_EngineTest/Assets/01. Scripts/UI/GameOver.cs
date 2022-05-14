using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class GameOver : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] GameObject gameOverPanel;

    private void Start()
    {
        StartCoroutine(FareWell());
    }

    private IEnumerator FareWell()
    {
        yield return new WaitForSecondsRealtime(1f);
        fadeImage.DOFade(1f, 1f).OnComplete(() => {
            fadeImage.DOFade(0f, 0f);
            gameOverPanel.SetActive(true);
        });
    }
}
