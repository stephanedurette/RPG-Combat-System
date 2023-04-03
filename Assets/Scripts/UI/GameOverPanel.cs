using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private RectTransform panelContent;
    [SerializeField] private Button retryButton;
    public void TogglePanel(bool open)
    {
        panelContent.gameObject.SetActive(open);
    }

    private void OnEnable()
    {
        retryButton.onClick.AddListener(ResetLevel);
    }

    private void OnDisable()
    {
        retryButton.onClick.RemoveListener(ResetLevel);
    }

    public void ResetLevel()
    {
        Singleton.Instance.LevelManager.RestartLevel();
    }
}
