using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Manual : MonoBehaviour
{
    [SerializeField] GameObject buttonCanvas;
    [SerializeField] GameObject manualPanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && manualPanel.activeSelf)
            ManualControl();
    }

    public void ManualControl()
    {
        if(buttonCanvas.activeSelf) OpenManual();
        else if(manualPanel.activeSelf) CloseManual();
    }

    private void OpenManual()
    {
        buttonCanvas.SetActive(false);
        manualPanel.SetActive(true);

        Sequence seq = DOTween.Sequence();
        seq.Append(manualPanel.transform.DOScale(new Vector3(1.1f, 1.1f), 0.5f));
        seq.Append(manualPanel.transform.DOScale(new Vector3(0.9f, 0.9f), 0.1f));
        seq.Append(manualPanel.transform.DOScale(new Vector3(1f, 1f), 0.1f));
    }

    private void CloseManual()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(manualPanel.transform.DOScale(new Vector3(0f, 0f), 0.5f)).OnComplete(() => {
            buttonCanvas.SetActive(true);
            manualPanel.SetActive(false);
        });
    }
}
