using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeTemplate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI orderRecipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;


    public void SetRecipeTextAndIcon(OrderRecipeSO orderRecipeSO) {
        orderRecipeNameText.text = orderRecipeSO.orderRecipeName + " +" + orderRecipeSO.score.ToString();

        foreach (Transform child in iconContainer) {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in orderRecipeSO.kitchenObjectSOList) {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.icon;
        }
    }
}
