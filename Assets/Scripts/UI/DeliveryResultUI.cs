using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Image icon;

    [SerializeField] private Color successColor;
    [SerializeField] private Color failColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failSprite;

    private Animator animator;
    private const string POP_UP = "Popup";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFail += DeliveryManager_OnRecipeFail;
        
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFail(object sender, System.EventArgs e) {
        gameObject.SetActive(true);
        animator.SetTrigger(POP_UP);
        background.color = failColor;
        icon.sprite = failSprite;
        messageText.text = "Delivery\nFailed";
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) {
        gameObject.SetActive(true);
        animator.SetTrigger(POP_UP);
        background.color = successColor;
        icon.sprite = successSprite;
        messageText.text = "Delivery\nSuccess";
    }
}
