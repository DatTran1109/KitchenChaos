using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Start() {
        DeliveryManager.Instance.OnRecipeSpawn += DeliveryManager_OnRecipeSpawn;
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawn(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in container) {
            if (child != recipeTemplate) {
                Destroy(child.gameObject);
            }
        }

        foreach (OrderRecipeSO orderRecipeSO in DeliveryManager.Instance.GetOrderRecipeSOList()) {
            Transform orderRecipeTransform = Instantiate(recipeTemplate, container);
            orderRecipeTransform.gameObject.SetActive(true);
            orderRecipeTransform.GetComponent<RecipeTemplate>().SetRecipeTextAndIcon(orderRecipeSO);
        }
    }
}
