using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    private int previousCountdownNumber;
    private const string NUMBER_POPUP = "NumberPopup";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        gameObject.SetActive(false);
    }

    private void Update() {
        int countdownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountdownTimer());
        countdownText.text = countdownNumber.ToString();

        if (previousCountdownNumber != countdownNumber ) {
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (KitchenGameManager.Instance.IsCountdownActive()) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
