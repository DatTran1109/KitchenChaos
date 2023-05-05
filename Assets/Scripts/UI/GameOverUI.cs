using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeDeliveredText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI lastRecordText;

    [SerializeField] private Button replayButton;
    [SerializeField] private Button mainMenuButton;

    private void Start() {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        gameObject.SetActive(false);

        replayButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (KitchenGameManager.Instance.IsGameOverActive()) {
            recipeDeliveredText.text = "Recipe Deliverd: " + DeliveryManager.Instance.GetSuccessRecipeAmount().ToString();
            scoreText.text = "Your Score: " + DeliveryManager.Instance.GetScore();
            lastRecordText.text = "Last Record: " + DeliveryManager.Instance.GetLastRecord();
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
