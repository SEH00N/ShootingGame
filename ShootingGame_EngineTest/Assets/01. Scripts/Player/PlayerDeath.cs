using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] Image fadeImage;

    private void OnDisable()
    {
        if(Player.Instance.hp <= 0.1 && !OnPause.Instance.isMain)
            fadeImage.DOFade(1f, 1f).OnComplete(() => {
                   SceneManager.LoadScene("GameOver");
                });
    }
}
