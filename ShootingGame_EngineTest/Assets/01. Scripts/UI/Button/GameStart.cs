using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameStart : MonoBehaviour
{
    [SerializeField] Image FadeImage;
    [SerializeField] GameObject buttonCanvas;

    public void StartGame()
    {
        buttonCanvas.SetActive(false);

        FadeImage.DOFade(1f, 1f).OnComplete(() => {
            SceneManager.LoadScene("InGame");
        });
    }
}
