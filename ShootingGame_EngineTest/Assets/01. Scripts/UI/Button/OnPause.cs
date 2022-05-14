using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class OnPause : MonoBehaviour
{
    public static OnPause Instance = null;

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject fadeImageCanvas;
    [SerializeField] Image fadeImage;
    public bool isMain = false;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            PauseOnOFF();
    }

    public void PauseOnOFF()
    {
        if(pauseButton.activeSelf) PauseGame();
        else if(pausePanel.activeSelf) ResumeGame();
    } 

    private void PauseGame()
    {
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);

        Sequence seq = DOTween.Sequence();
        seq.Append(pausePanel.transform.DOScale(new Vector3(1.2f, 1.2f), 0.5f));
        seq.Append(pausePanel.transform.DOScale(new Vector3(1f, 1f), 0.1f)).OnComplete(() => {
            Time.timeScale = 0f;
        });
    }

    private void ResumeGame()
    {
        isMain = true;
        Time.timeScale = 1f;

        Sequence seq = DOTween.Sequence();
        seq.Append(pausePanel.transform.DOScale(new Vector3(0, 0), 0.5f)).OnComplete(() => {
            pausePanel.SetActive(false);
            pauseButton.SetActive(true);
        });
    }

    public void ToMain()
    {
        fadeImageCanvas.SetActive(true);
        Time.timeScale = 1f;

        fadeImage.DOFade(1f, 0.5f).OnComplete(() => {
            SceneManager.LoadScene("MainMenu");
        });
    }
}
