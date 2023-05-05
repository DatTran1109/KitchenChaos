using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            KitchenGameManager.Instance.TogglePauseGame();
        });
        optionButton.onClick.AddListener(() => {
            OptionUI.Instance.gameObject.SetActive(true);
        });
        mainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1f;
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start() {
        KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        gameObject.SetActive(false);
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        gameObject.SetActive(false);
    }

    private void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e) {
        gameObject.SetActive(true);
    }
}
