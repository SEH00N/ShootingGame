using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class OnPause : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject pauseButton;
    [SerializeField] Image fadeIamage;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        PuaseOnOFF();
    }

    public void PuaseOnOFF()
    {
        if(pauseButton.activeSelf) PauseGame();
        else if(pausePanel.activeSelf) ResumeGame();
    } 

    private void PauseGame()
    {
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);

        Sequence seq = DOTween.Sequence();
        seq.Append(pausePanel.transform.DOScale(new Vector3(0f, 0f), 0f));
        seq.Append(pausePanel.transform.DOScale(new Vector3(1.2f, 1.2f), 0.5f));
        seq.Append(pausePanel.transform.DOScale(new Vector3(1f, 1f), 0.1f)).OnComplete(() => {
            Time.timeScale = 0f;
        });
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;

        Sequence seq = DOTween.Sequence();
        seq.Append(pausePanel.transform.DOScale(new Vector3(0, 0), 0.5f)).OnComplete(() => {
            pausePanel.SetActive(false);
            pauseButton.SetActive(true);
        });
    }

    public void ToMain()
    {
        fadeIamage.DOFade(1f, 0.5f).OnComplete(() => {
            SceneManager.LoadScene("Main");
        });
    }
}
