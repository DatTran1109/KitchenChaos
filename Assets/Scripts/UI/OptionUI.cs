using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }

    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;

    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;

    [SerializeField] private Transform pressToRebind;

    private void Awake() {
        Instance = this;

        soundEffectButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });

        moveUpButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.MoveUp);
        });
        moveDownButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.MoveDown);
        });
        moveLeftButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.MoveLeft);
        });
        moveRightButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.MoveRight);
        });
        interactButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Interact);
        });
        interactAltButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.InteractAlt);
        });
        pauseButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Pause);
        });
    }

    private void Start() {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        UpdateVisual();
        gameObject.SetActive(false);
        pressToRebind.gameObject.SetActive(false);
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        gameObject.SetActive(false);
    }

    private void UpdateVisual() {
        soundEffectText.text = "Sound Effect: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    private void RebindBinding(GameInput.Binding binding) {
        pressToRebind.gameObject.SetActive(true);
        GameInput.Instance.RebindBinding(binding, () => {
            pressToRebind.gameObject.SetActive(false);
            UpdateVisual();
        });
    }
}
